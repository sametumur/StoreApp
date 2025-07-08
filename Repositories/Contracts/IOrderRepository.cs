using Entities.Models;

namespace Repositories.Contracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
    IQueryable<Order> GetAllOrders {get; }
    Order? GetOrder(int id, bool trackChanges);
    void CreateOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
    void CompleteOrder(int id);
    int NumberOfOrders {get; }
}