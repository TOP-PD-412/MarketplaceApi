using ProductsApi.Core.Infrastructure.Db.Mappers;
using ProductsApi.Core.Utils;
using ProductsApi.Modules.Products.Db.Entities;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Db.Mappers;

public sealed class ProductMapper : MapperBase<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.PreviewUrl = model.PreviewUrl?.ToString();
        return entity;
    }

    public override ProductModel MapToModel(ProductEntity entity) =>
        base.MapToModel(entity) with
        {
            Name = entity.Name,
            PreviewUrl = entity.PreviewUrl?.ToUri()
        };
}