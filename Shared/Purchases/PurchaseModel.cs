using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Purchases;

public sealed record PurchaseModel : Model
{
    public Guid BuyerId { get; init; }
    public Guid SellerId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public BigInteger PricePaid { get; init; }
}