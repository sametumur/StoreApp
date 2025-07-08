using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ProductManager :  IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    
    private readonly IMapper _mapper;

    public ProductManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public IEnumerable<Product> GetAllProducts(bool trackChanges)
    {
        return _repositoryManager.Products.GetAllProducts(trackChanges);
    }

    public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
    {
        return _repositoryManager.Products.GetShowCaseProducts(trackChanges);
    }

    public IEnumerable<Product> GetLatestProducts(int count, bool trackChanges)
    {
       return _repositoryManager.Products.FindAll(trackChanges)
            .OrderByDescending(p => p.Id)
            .Take(count);
    }

    public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters? productRequestParameters)
    {
        return _repositoryManager.Products.GetAllProductsWithDetails(productRequestParameters);
    }

    public Product? GetProduct(int id, bool trackChanges)
    {
        var product = _repositoryManager.Products.GetProduct(id, trackChanges);
        if (product == null)
            throw new Exception("Product not found");
        
        return product;
    }
    
    public ProductDtoForUpdate GetProductForUpdate(int id, bool trackChanges)
    {
        var product = _repositoryManager.Products.GetProduct(id, trackChanges);
        return _mapper.Map<ProductDtoForUpdate>(product);
    }

    public void CreateProduct(ProductDtoForInsertion productDto)
    {
      Product product = _mapper.Map<Product>(productDto);
      _repositoryManager.Products.CreateProduct(product);
      _repositoryManager.Save();
    }
    

    public void UpdateProduct(ProductDtoForUpdate productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        _repositoryManager.Products.UpdateProduct(product);
        _repositoryManager.Save();
    }

    public void DeleteProduct(int id)
    {
        Product product = _repositoryManager.Products.GetProduct(id, false);
        if (product is not null)
        {
            _repositoryManager.Products.DeleteProduct(product);
            _repositoryManager.Save();
        }
    }
}