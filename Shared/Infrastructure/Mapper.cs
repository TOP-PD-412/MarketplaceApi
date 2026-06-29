namespace Shared.Infrastructure;

public abstract class Mapper<TModel, TEntity>
    where TEntity : Entity, new()
    where TModel : Model, new()
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