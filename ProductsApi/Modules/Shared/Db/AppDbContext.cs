using Microsoft.EntityFrameworkCore;
using ProductsApi.Core.Constants;
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
            entity.ToTable("products")
                .HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(Limits.Product.Name.MaxLength)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .ValueGeneratedNever();

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired()
                .ValueGeneratedNever();
        });
    }
}