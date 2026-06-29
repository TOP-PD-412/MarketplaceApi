using System.ComponentModel.DataAnnotations;

namespace PurchaseApi.Purchase;

public sealed record CreatePurchaseRequest
{
    [Required]
    public required Guid ProductId { get; init; }
}