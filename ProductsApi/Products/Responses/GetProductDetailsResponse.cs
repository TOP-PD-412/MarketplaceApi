using System.Collections.Frozen;

namespace ProductsApi.Products.Responses;

public sealed record GetProductDetailsResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string[] ImageUrls { get; init; } = [];
    public required string Price { get; init; }
    public required int Amount { get; init; }
    public required FrozenDictionary<string, string> Characteristics { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}