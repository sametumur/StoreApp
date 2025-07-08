using Entities.Models;

namespace Repositories.Contracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    
    Category? GetCategory(int id, bool trackChanges);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
    
}