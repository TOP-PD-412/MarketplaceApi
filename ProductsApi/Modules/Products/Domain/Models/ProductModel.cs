using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Modules.Products.Domain.Models;

public sealed record ProductModel : ModelBase
{
    public string Name { get; init; } = string.Empty;

    public ProductModel WithUpdatedName(string name) =>
        Touch<ProductModel>() with
        {
            Name = name,
        };
}