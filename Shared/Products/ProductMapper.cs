using System.Collections.Frozen;
using Shared.Infrastructure;
using Shared.Utils;

namespace Shared.Products;

public sealed class ProductMapper : Mapper<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.ImageUrls = model.ImageUrls.Select(url => url.ToString()).ToArray();
        entity.Characteristics = model.Characteristics.ToDictionary();
        entity.Amount = model.Amount;
        entity.Price = model.Price;
        entity.SellerId = model.SellerId;
        return entity;
    }

    public override ProductModel MapToModel(ProductEntity entity) =>
        base.MapToModel(entity) with
        {
            Name = entity.Name,
            Description = entity.Description,
            ImageUrls = entity.ImageUrls.Select(url => url.ToUri()!).ToArray(),
            Characteristics = entity.Characteristics.ToFrozenDictionary(),
            Amount = entity.Amount,
            Price = entity.Price,
            SellerId = entity.SellerId
        };
}