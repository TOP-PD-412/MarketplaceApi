using ProductsApi.Core.Infrastructure.Db.Mappers;
using ProductsApi.Modules.Products.Db.Entities;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Db.Mappers;

public sealed class ProductMapper : MapperBase<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model) =>
        new()
        {
            Name = model.Name,
        };

    public override ProductModel MapToModel(ProductEntity entity)
    {
        var model = base.MapToModel(entity);
        model.Name = entity.Name;
        return model;
    }
}