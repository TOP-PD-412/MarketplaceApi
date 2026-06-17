namespace ProductsApi.Core.Infrastructure.Db.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public abstract class EntityBase<TEntity> : EntityBase
    where TEntity : EntityBase<TEntity>
{
    public virtual void Update(TEntity other)
    {
        UpdatedAt = other.UpdatedAt;
    }
}