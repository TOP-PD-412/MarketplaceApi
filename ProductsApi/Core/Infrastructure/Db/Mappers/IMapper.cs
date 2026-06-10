using ProductsApi.Core.Infrastructure.Db.Entities;
using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Core.Infrastructure.Db.Mappers;

public interface IMapper<TModel, TEntity>
    where TEntity : EntityBase, new()
    where TModel : ModelBase, new()
{
    TEntity MapToEntity(TModel model);
    TModel MapToModel(TEntity entity);
}