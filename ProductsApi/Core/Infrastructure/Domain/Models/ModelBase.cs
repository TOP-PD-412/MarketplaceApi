namespace ProductsApi.Core.Infrastructure.Domain.Models;

public abstract class ModelBase
{
    public Guid Id { get; init; }

    public override bool Equals(object? obj) =>
        obj is ModelBase other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}