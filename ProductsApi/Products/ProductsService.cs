using ProductsApi.Products.Requests;
using ProductsApi.Products.Responses;
using Shared.Products;

namespace ProductsApi.Products;

public sealed class ProductsService(ProductsRepo productsRepo)
{
    public async Task<GetProductDetailsResponse?> GetProductDetailsAsync(Guid id)
    {
        var product = await productsRepo.FindByIdAsync(id);
        return product?.ConvertToGetProductDetailsResponse();
    }

    public async Task<IEnumerable<GetProductPreviewResponse>> GetAllProductPreviewsAsync()
    {
        var products = await productsRepo.FindAllAsync();
        return products.Select(ProductConverter.ConvertToGetProductPreviewResponse);
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