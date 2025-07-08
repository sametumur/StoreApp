namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IProductRepository Products { get; }
    ICategoryRepository Category { get; }
    IOrderRepository Order { get; }
    
    void Save();
}