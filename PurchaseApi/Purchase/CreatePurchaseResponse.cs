namespace PurchaseApi.Purchase;

public sealed record CreatePurchaseResponse
{
    public required Guid PurchaseId { get; init; }
}