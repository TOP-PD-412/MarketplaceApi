using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsApi.Core.Infrastructure.Db.Entities;

namespace ProductsApi.Core.Utils.Db;

public static class EntityTypeBuilderEx
{
    public static EntityTypeBuilder<TEntity> ToTableWithDefaultProps<TEntity>(this EntityTypeBuilder<TEntity> entity,
        string tableName) where TEntity : EntityBase
    {
        entity.ToTable(tableName)
            .HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        entity.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .ValueGeneratedNever();

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired()
            .ValueGeneratedNever();

        return entity;
    }
}