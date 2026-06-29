using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure;

public abstract class Repo<TModel, TEntity>(
    DbContext ctx,
    DbSet<TEntity> table,
    Mapper<TModel, TEntity> mapper
)
    where TEntity : Entity<TEntity>, new()
    where TModel : Model, new()
{
    public async Task<TModel?> FindByIdAsync(Guid id)
    {
        var entity = await table.FindAsync(id);
        return entity == null ? null : mapper.MapToModel(entity);
    }

    public async Task<TModel?> FindByIdAndLockAsync(Guid id)
    {
        var query = $"""
                     SELECT *
                     FROM {table.EntityType.GetTableName()}
                     WHERE id = @p0
                     FOR UPDATE
                     """;
        var entity = await table.FromSqlRaw(query, id)
            .FirstOrDefaultAsync();
        return entity == null ? null : mapper.MapToModel(entity);
    }

    public async Task<IEnumerable<TModel>> FindAllAsync() =>
        await table.AsNoTracking()
            .Select(entity => mapper.MapToModel(entity))
            .ToArrayAsync();

    public async Task<bool> ContainsByIdAsync(Guid id)
    {
        var entity = await table.FindAsync(id);
        return entity != null;
    }

    public async Task AddAsync(TModel model)
    {
        var entity = mapper.MapToEntity(model);
        await table.AddAsync(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(TModel model)
    {
        var entity = await table.FindAsync(model.Id) ?? throw new KeyNotFoundException();
        entity.Update(mapper.MapToEntity(model));
        await ctx.SaveChangesAsync();
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var entity = await table.FindAsync(id) ?? throw new KeyNotFoundException();
        table.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    protected async Task<TModel?> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await table.AsNoTracking().FirstOrDefaultAsync(predicate);
        return entity == null ? null : mapper.MapToModel(entity);
    }

    protected async Task<IEnumerable<TModel>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate) =>
        await table.AsNoTracking()
            .Where(predicate)
            .Select(entity => mapper.MapToModel(entity))
            .ToArrayAsync();
}