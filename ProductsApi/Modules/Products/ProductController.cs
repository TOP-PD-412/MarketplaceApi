using Microsoft.AspNetCore.Mvc;
using ProductsApi.Modules.Products.Dtos.Responses;
using ProductsApi.Modules.Products.Services;

namespace ProductsApi.Modules.Products;

[ApiController]
[Route("api/product")]
public sealed class ProductController(IProductsService productsService) : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GetProductResponse>> GetProductAsync([FromRoute] Guid id)
    {
        var product = await productsService.GetProductAsync(id);
        if (product == null) return NotFound();
        return Ok(GetProductResponse.CreateFrom(product));
    }
}