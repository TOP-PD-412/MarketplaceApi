using Microsoft.AspNetCore.Mvc;
using ProductsApi.Modules.Products.Dtos.Requests;
using ProductsApi.Modules.Products.Dtos.Responses;
using ProductsApi.Modules.Products.Services;

namespace ProductsApi.Modules.Products;

[ApiController]
[Route("api/product")]
public sealed class ProductController(IProductsService productsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var product = request.ConvertToProduct();
        await productsService.AddProductAsync(product);
        return Created();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GetProductResponse>> GetProductAsync([FromRoute] Guid id)
    {
        var product = await productsService.GetProductAsync(id);
        if (product == null) return NotFound();
        return Ok(GetProductResponse.CreateFrom(product));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = request.ConvertToProduct(id);
        try
        {
            await productsService.UpdateProductAsync(product);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteProductAsync([FromRoute] Guid id)
    {
        try
        {
            await productsService.RemoveProductAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}