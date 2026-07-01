using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseApi.Constants;
using Shared.Users;

namespace PurchaseApi.Purchase;

[ApiController]
[Authorize]
[Route("api/purchase")]
public sealed class PurchaseController(PurchasesService purchasesService) : ControllerBase
{
    [HttpPost(Name = Routes.Purchase.Create)]
    [Authorize(Roles = UserRole.Buyer)]
    public async Task<ActionResult> CreatePurchaseAsync([FromBody] CreatePurchaseRequest request)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var buyerId))
            return Unauthorized();

        try
        {
            var response = await purchasesService.CretePurchaseAsync(request, buyerId);
            return CreatedAtRoute(Routes.Purchase.Get, new { id = response.PurchaseId }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex) when (ex is NotEnoughBalanceException or AlreadySoldException)
        {
            return BadRequest(ex.Message);
        }
        catch (SellingToSelfException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("{id:guid}", Name = Routes.Purchase.Get)]
    public async Task<ActionResult<GetPurchaseResponse>> GetPurchaseAsync([FromRoute] Guid id)
    {
        var response = await purchasesService.GetPurchaseAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }
}