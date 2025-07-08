using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context) : base(context)
    {
        
    }
    
    public IQueryable<Category> GetAllCategories(bool trackChanges) => FindAll(trackChanges);

    public Category? GetCategory(int id, bool trackChanges) => FindByCondition(p => p.Id == id, trackChanges);
    
    public void CreateCategory(Category category) => Add(category);
    

    public void UpdateCategory(Category category) => Update(category);
    

    public void DeleteCategory(Category category) => Delete(category);
}