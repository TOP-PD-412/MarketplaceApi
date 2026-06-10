using ProductsApi.Core.Infrastructure.Db.Entities;
using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Core.Infrastructure.Db.Mappers;

public abstract class MapperBase<TModel, TEntity> : IMapper<TModel, TEntity>
    where TEntity : EntityBase, new()
    where TModel : ModelBase, new()
{
    public abstract TEntity MapToEntity(TModel model);

    public virtual TModel MapToModel(TEntity entity) =>
        new()
        {
            Id = entity.Id
        };
}