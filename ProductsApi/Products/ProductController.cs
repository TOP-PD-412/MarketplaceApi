using Microsoft.AspNetCore.Mvc;
using ProductsApi.Constants;
using ProductsApi.Products.Requests;
using ProductsApi.Products.Responses;

namespace ProductsApi.Products;

[ApiController]
[Route("api/product")]
public sealed class ProductController(ProductsService productsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateProductResponse>> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var response = await productsService.AddProductAsync(request);
        return CreatedAtRoute(Routes.Product.Get, new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}", Name = Routes.Product.Get)]
    public async Task<ActionResult<GetProductDetailsResponse>> GetProductAsync([FromRoute] Guid id)
    {
        var response = await productsService.GetProductDetailsAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPut("{id:guid}", Name = Routes.Product.Update)]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProductAsync([FromRoute] Guid id,
        [FromBody] UpdateProductRequest request)
    {
        try
        {
            var response = await productsService.UpdateProductAsync(request, id);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}", Name = Routes.Product.Delete)]
    public async Task<ActionResult> DeleteProductAsync([FromRoute] Guid id)
    {
        try
        {
            await productsService.RemoveProductAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}