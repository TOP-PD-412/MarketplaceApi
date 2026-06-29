using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Infrastructure;

public abstract class Entity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public abstract class Entity<TEntity> : Entity
    where TEntity : Entity<TEntity>
{
    public virtual void Update(TEntity other)
    {
        UpdatedAt = other.UpdatedAt;
    }
}