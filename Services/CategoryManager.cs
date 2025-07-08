using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CategoryManger :  ICategoryService
{
    private readonly IRepositoryManager _repositoryManager;
    
    private readonly IMapper _mapper;

    public CategoryManger(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }


    public IEnumerable<Category> GetAllCategories(bool trackChanges)
    {
       return _repositoryManager.Category.FindAll(trackChanges);
    }

    public Category? GetCategory(int id, bool trackChanges)
    {
        Category? category = _repositoryManager.Category.GetCategory(id, trackChanges);
        if (category == null)
            throw new Exception("Category not found");
        
        return category;
    }
    
    public CategoryDtoForUpdate GetCategoryForUpdate(int id, bool trackChanges)
    {
        Category category = _repositoryManager.Category.GetCategory(id, trackChanges);
        if (category == null)
            throw new Exception("Category not found");
        var categoryDto = _mapper.Map<CategoryDtoForUpdate>(category);
        return categoryDto;
    }

    public void CreateCategory(CategoryDtoForInsertion categoryDto)
    {
        Category category = _mapper.Map<Category>(categoryDto);
        _repositoryManager.Category.CreateCategory(category);
        _repositoryManager.Save();
    }

    public void UpdateCategory(CategoryDtoForUpdate categoryDto)
    {
        Category category = _mapper.Map<Category>(categoryDto);
        _repositoryManager.Category.UpdateCategory(category);
        _repositoryManager.Save();
    }

    public void DeleteCategory(int id)
    {
        Category? category = _repositoryManager.Category.GetCategory(id, false);
        if (category is not null)
        {
            _repositoryManager.Category.DeleteCategory(category);
            _repositoryManager.Save();
        }
    }
}