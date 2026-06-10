using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Services;

public interface IProductsService
{
    Task<ProductModel?> GetProductAsync(Guid id);
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task AddProductAsync(ProductModel product);
    Task UpdateProductAsync(ProductModel product);
    Task RemoveProductAsync(Guid id);
}