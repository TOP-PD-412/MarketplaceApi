using System.Collections.Frozen;
using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Petitions;

public abstract record PetitionModel() : Model
{
    public PetitionType Type { get; init; }
    public PetitionStatus Status { get; init; } = PetitionStatus.Pending;

    protected PetitionModel(PetitionType type) : this() => Type = type;
}

public sealed record CreateSellerPetitionModel() : PetitionModel(PetitionType.CreateSeller)
{
    public string Name { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
}

public sealed record CreateProductPetitionModel() : PetitionModel(PetitionType.CreateProduct)
{
    public Guid SellerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Uri[] ImageUrls { get; init; } = [];
    public FrozenDictionary<string, string> Characteristics { get; init; } = FrozenDictionary<string, string>.Empty;
    public BigInteger Price { get; init; }
    public int Amount { get; init; }
}