using ProductsApi.Core.Infrastructure.Db.Entities;
using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Core.Infrastructure.Db.Mappers;

public abstract class MapperBase<TModel, TEntity> : IMapper<TModel, TEntity>
    where TEntity : EntityBase, new()
    where TModel : ModelBase, new()
{
    public virtual TEntity MapToEntity(TModel model) =>
        new()
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
        };

    public virtual TModel MapToModel(TEntity entity) =>
        new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
}