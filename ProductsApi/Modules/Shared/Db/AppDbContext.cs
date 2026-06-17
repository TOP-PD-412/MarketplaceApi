using Microsoft.EntityFrameworkCore;
using ProductsApi.Core.Constants;
using ProductsApi.Core.Utils.Db;
using ProductsApi.Modules.Products.Db.Entities;

namespace ProductsApi.Modules.Shared.Db;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.ToTableWithDefaultProps("products");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(Limits.Product.Name.MaxLength)
                .IsRequired();

            entity.Property(e => e.PreviewUrl)
                .HasColumnName("preview_url");
        });
    }
}