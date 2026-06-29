namespace PurchaseApi.Purchase;

public sealed record GetPurchaseResponse
{
    public required string ProductName { get; init; }
    public required string PricePaid { get; init; }
    public required Guid SellerId { get; init; }
    public required DateTime SoldAt { get; init; }
}