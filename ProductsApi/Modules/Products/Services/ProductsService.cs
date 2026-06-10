using ProductsApi.Modules.Products.Db.Repos;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Services;

public sealed class ProductsService(IProductsRepo productsRepo) : IProductsService
{
    public async Task<ProductModel?> GetProductAsync(Guid id) => await productsRepo.FindByIdAsync(id);
}