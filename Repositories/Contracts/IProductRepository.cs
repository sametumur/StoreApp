using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts;

public interface IProductRepository: IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChanges);
    IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters? productRequestParameters);
    IQueryable<Product> GetShowCaseProducts(bool trackChanges);
    Product? GetProduct(int id, bool trackChanges);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}