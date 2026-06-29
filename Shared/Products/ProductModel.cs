using System.Collections.Frozen;
using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Products;

public sealed record ProductModel : Model
{
    public Guid SellerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Uri[] ImageUrls { get; init; } = [];
    public FrozenDictionary<string, string> Characteristics { get; init; } = FrozenDictionary<string, string>.Empty;
    public BigInteger Price { get; init; }
    public int Amount { get; init; }

    public ProductModel WithUpdatedName(string name) =>
        Touch<ProductModel>() with
        {
            Name = name,
        };

    public ProductModel WithDecreasedAmount(int dec = 1) =>
        Touch<ProductModel>() with
        {
            Amount = Amount - dec,
        };

    //TODO: реализовать остальные функции обновления
}