using Microsoft.EntityFrameworkCore;
using Shared.Petitions;
using Shared.Products;
using Shared.Purchases;
using Shared.Users;

namespace Shared.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PurchaseEntity> Purchases => Set<PurchaseEntity>();
    public DbSet<PetitionEntity> Petitions => Set<PetitionEntity>();
}