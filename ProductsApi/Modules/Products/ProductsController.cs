using Microsoft.AspNetCore.Mvc;
using ProductsApi.Core.Constants;
using ProductsApi.Modules.Products.Dtos.Responses;
using ProductsApi.Modules.Products.Services;

namespace ProductsApi.Modules.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(IProductsService productsService) : ControllerBase
{
    [HttpGet("all", Name = Routes.Products.GetAll)]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductsAsync()
    {
        var response = await productsService.GetAllProductsAsync();
        return Ok(response);
    }
}