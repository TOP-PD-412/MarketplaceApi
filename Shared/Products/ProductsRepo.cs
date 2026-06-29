using Shared.Infrastructure;

namespace Shared.Products;

public sealed class ProductsRepo(AppDbContext ctx, ProductMapper mapper)
    : Repo<ProductModel, ProductEntity>(ctx, ctx.Products, mapper)
{
}