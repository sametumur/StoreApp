using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);
    IEnumerable<Product> GetShowCaseProducts(bool trackChanges);
    IEnumerable<Product> GetLatestProducts(int count, bool trackChanges);
    IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters? productRequestParameters);
    Product? GetProduct(int id, bool trackChanges);
    void CreateProduct(ProductDtoForInsertion productDto);
    void UpdateProduct(ProductDtoForUpdate productDto);
    void DeleteProduct(int id);
    ProductDtoForUpdate GetProductForUpdate(int id, bool trackChanges);
}