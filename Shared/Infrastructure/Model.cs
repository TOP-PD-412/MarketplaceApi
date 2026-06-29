namespace Shared.Infrastructure;

public abstract record Model
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    protected TSelf Touch<TSelf>() where TSelf : Model =>
        (TSelf)this with
        {
            UpdatedAt = DateTime.UtcNow
        };
}