using Microsoft.AspNetCore.Mvc;
using ProductsApi.Modules.Products.Dtos.Responses;
using ProductsApi.Modules.Products.Services;

namespace ProductsApi.Modules.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(IProductsService productsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductsAsync()
    {
        var products = await productsService.GetAllProductsAsync();
        return Ok(products.Select(GetProductResponse.CreateFrom));
    }
}