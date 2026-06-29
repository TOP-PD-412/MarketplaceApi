namespace ProductsApi.Products.Responses;

public sealed record GetProductPreviewResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public string? PreviewUrl { get; init; }
    public required string Price { get; init; }
    public required int Amount { get; init; }
}