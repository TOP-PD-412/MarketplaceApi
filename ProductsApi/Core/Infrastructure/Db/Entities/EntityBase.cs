namespace ProductsApi.Core.Infrastructure.Db.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }
}

public abstract class EntityBase<TEntity> : EntityBase
    where TEntity : EntityBase<TEntity>
{
    public abstract void Update(TEntity other);
}