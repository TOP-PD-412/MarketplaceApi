using Shared.Purchases;

namespace PurchaseApi.Purchase;

public static class PurchaseConverter
{
    extension(PurchaseModel purchase)
    {
        public CreatePurchaseResponse ConvertToCreatePurchaseResponse() => new()
        {
            PurchaseId = purchase.Id,
        };

        public GetPurchaseResponse ConvertToGetPurchaseResponse() => new()
        {
            PricePaid = purchase.PricePaid.ToString(),
            ProductName = purchase.ProductName,
            SellerId = purchase.SellerId,
            SoldAt = purchase.CreatedAt,
        };
    }
}