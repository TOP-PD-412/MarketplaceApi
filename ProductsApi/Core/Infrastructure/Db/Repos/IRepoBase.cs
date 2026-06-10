using ProductsApi.Core.Infrastructure.Domain.Models;

namespace ProductsApi.Core.Infrastructure.Db.Repos;

public interface IRepoBase<TModel> where TModel : ModelBase, new()
{
    Task<TModel?> FindByIdAsync(Guid id);
    Task<IEnumerable<TModel>> FindAllAsync();
    Task<bool> ContainsByIdAsync(Guid id);
    
    Task AddAsync(TModel model);
    
    Task UpdateAsync(TModel model);
    
    Task RemoveByIdAsync(Guid id);
}