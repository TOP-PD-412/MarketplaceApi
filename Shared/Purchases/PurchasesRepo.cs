using PurchaseApi.Purchase;
using Shared.Infrastructure;

namespace Shared.Purchases;

public sealed class PurchasesRepo(AppDbContext ctx, PurchaseMapper mapper)
    : Repo<PurchaseModel, PurchaseEntity>(ctx, ctx.Purchases, mapper)
{
}