using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context) : base(context)
    {
        
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);
    public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters? productRequestParameters)
    {
        return _context.Products
            .FilteredByCategoryId(productRequestParameters?.CategoryId)
            .FilteredBySearchTerm(productRequestParameters?.SearchTerm)
            .FilteredByPrice(productRequestParameters?.MinPrice, productRequestParameters?.MaxPrice,
                productRequestParameters.IsValidPrice)
            .ToPagedList(productRequestParameters.Page, productRequestParameters.PageSize);
    }

    public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
    {
        return FindAll(trackChanges).Where(p => p.Showcase.Equals(true));
    }

    public Product GetProduct(int id, bool trackChanges) => FindByCondition(p => p.Id == id, trackChanges);
    
    public void CreateProduct(Product product) => Add(product);
    
    public void UpdateProduct(Product product) => Update(product);
    
    public void DeleteProduct(Product product) => Delete(product);
}