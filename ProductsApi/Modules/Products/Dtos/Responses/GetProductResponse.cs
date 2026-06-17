namespace ProductsApi.Modules.Products.Dtos.Responses;

public sealed record GetProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public string? PreviewUrl { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}