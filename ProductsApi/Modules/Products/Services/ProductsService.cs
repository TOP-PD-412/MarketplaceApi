using ProductsApi.Modules.Products.Db.Repos;
using ProductsApi.Modules.Products.Domain.Converters;
using ProductsApi.Modules.Products.Dtos.Requests;
using ProductsApi.Modules.Products.Dtos.Responses;

namespace ProductsApi.Modules.Products.Services;

public sealed class ProductsService(IProductsRepo productsRepo) : IProductsService
{
    public async Task<GetProductResponse?> GetProductAsync(Guid id)
    {
        var product = await productsRepo.FindByIdAsync(id);
        return product?.ConvertToGetProductResponse();
    }

    public async Task<IEnumerable<GetProductResponse>> GetAllProductsAsync()
    {
        var products = await productsRepo.FindAllAsync();
        return products.Select(ProductConverter.ConvertToGetProductResponse);
    }

    public async Task<CreateProductResponse> AddProductAsync(CreateProductRequest request)
    {
        var product = request.ConvertToProduct();
        await productsRepo.AddAsync(product);
        return product.ConvertToCreateProductResponse();
    }

    public async Task<UpdateProductResponse> UpdateProductAsync(UpdateProductRequest request, Guid id)
    {
        var product = request.ConvertToProduct(id);
        await productsRepo.UpdateAsync(product);
        return product.ConvertToUpdateProductResponse();
    }

    public async Task RemoveProductAsync(Guid id) => await productsRepo.RemoveByIdAsync(id);
}