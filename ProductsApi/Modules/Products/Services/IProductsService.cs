using ProductsApi.Modules.Products.Dtos.Requests;
using ProductsApi.Modules.Products.Dtos.Responses;

namespace ProductsApi.Modules.Products.Services;

public interface IProductsService
{
    Task<GetProductResponse?> GetProductAsync(Guid id);
    Task<IEnumerable<GetProductResponse>> GetAllProductsAsync();
    Task<CreateProductResponse> AddProductAsync(CreateProductRequest request);
    Task<UpdateProductResponse> UpdateProductAsync(UpdateProductRequest request, Guid id);
    Task RemoveProductAsync(Guid id);
}