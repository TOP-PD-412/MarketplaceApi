using ProductsApi.Core.Infrastructure.Db.Mappers;
using ProductsApi.Core.Infrastructure.Db.Repos;
using ProductsApi.Modules.Products.Db.Entities;
using ProductsApi.Modules.Products.Domain.Models;
using ProductsApi.Modules.Shared.Db;

namespace ProductsApi.Modules.Products.Db.Repos;

public interface IProductsRepo : IRepoBase<ProductModel>;

public sealed class ProductsRepo(AppDbContext ctx, IMapper<ProductModel, ProductEntity> mapper)
    : RepoBase<ProductModel, ProductEntity>(ctx, ctx.Products, mapper), IProductsRepo;