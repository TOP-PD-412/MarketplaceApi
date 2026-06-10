using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Modules.Products.Domain.Models;

public sealed class ProductModel : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public override bool Equals(object? obj) =>
        obj is ProductModel
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}