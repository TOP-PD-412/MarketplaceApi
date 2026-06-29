using ProductsApi.Products.Requests;
using ProductsApi.Products.Responses;
using Shared.Products;

namespace ProductsApi.Products;

public static class ProductConverter
{
    extension(ProductModel product)
    {
        public GetProductPreviewResponse ConvertToGetProductPreviewResponse() =>
            new()
            {
                Id = product.Id,
                Name = product.Name,
                PreviewUrl = product.ImageUrls.FirstOrDefault()?.ToString(),
                Amount = product.Amount,
                Price = product.Price.ToString()
            };

        public GetProductDetailsResponse ConvertToGetProductDetailsResponse() =>
            new()
            {
                Id = product.Id,
                ImageUrls = product.ImageUrls.Select(url => url.ToString()).ToArray(),
                Name = product.Name,
                Description = product.Description,
                Characteristics = product.Characteristics,
                Amount = product.Amount,
                Price = product.Price.ToString(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };

        public UpdateProductResponse ConvertToUpdateProductResponse() =>
            new()
            {
                UpdatedAt = product.UpdatedAt
            };

        public CreateProductResponse ConvertToCreateProductResponse() =>
            new()
            {
                Id = product.Id,
                CreatedAt = product.CreatedAt,
            };
    }

    public static ProductModel ConvertToProduct(this CreateProductRequest request) =>
        new()
        {
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public static ProductModel ConvertToProduct(this UpdateProductRequest request, Guid id) =>
        new ProductModel
            {
                Id = id
            }.WithUpdatedName(request.Name);
}