using Microsoft.AspNetCore.Mvc;
using ProductsApi.Constants;
using ProductsApi.Products.Responses;

namespace ProductsApi.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(ProductsService productsService) : ControllerBase
{
    [HttpGet("all", Name = Routes.Products.GetAll)]
    public async Task<ActionResult<IEnumerable<GetProductPreviewResponse>>> GetAllProductsAsync()
    {
        var response = await productsService.GetAllProductPreviewsAsync();
        return Ok(response);
    }
}