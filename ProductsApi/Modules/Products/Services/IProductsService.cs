using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Services;

public interface IProductsService
{
    Task<ProductModel?> GetProductAsync(Guid id);
}