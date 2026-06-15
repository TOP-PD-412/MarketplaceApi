namespace ProductsApi.Modules.Products.Dtos.Responses;

public sealed record UpdateProductResponse
{
    public required DateTime UpdatedAt { get; init; }
}