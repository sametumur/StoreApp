using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories(bool trackChanges);
    Category? GetCategory(int id, bool trackChanges);
    void CreateCategory(CategoryDtoForInsertion categoryDto);
    void UpdateCategory(CategoryDtoForUpdate category);
    void DeleteCategory(int id);
    CategoryDtoForUpdate GetCategoryForUpdate(int id, bool trackChanges);
}