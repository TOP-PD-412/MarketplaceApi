using Shared.Infrastructure;
using Shared.Purchases;

namespace PurchaseApi.Purchase;

public sealed class PurchaseMapper : Mapper<PurchaseModel, PurchaseEntity>
{
    public override PurchaseEntity MapToEntity(PurchaseModel model)
    {
        var entity = base.MapToEntity(model);
        entity.BuyerId = model.BuyerId;
        entity.SellerId = model.SellerId;
        entity.PricePaid = model.PricePaid;
        entity.ProductName = model.ProductName;
        return entity;
    }

    public override PurchaseModel MapToModel(PurchaseEntity entity)
    {
        return base.MapToModel(entity) with
        {
            BuyerId = entity.BuyerId,
            SellerId = entity.SellerId,
            PricePaid = entity.PricePaid,
            ProductName = entity.ProductName
        };
    }
}