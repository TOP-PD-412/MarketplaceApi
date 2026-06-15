namespace ProductsApi.Core.Infrastructure.Domain.Models;

public abstract record ModelBase
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    protected TSelf Touch<TSelf>() where TSelf : ModelBase =>
        (TSelf)this with
        {
            UpdatedAt = DateTime.UtcNow
        };
}