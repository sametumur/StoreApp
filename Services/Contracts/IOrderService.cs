using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface IOrderService
{
    
    IQueryable<Order> GetAllOrders {get; }
    Order? GetOrder(int id, bool trackChanges);
    void CreateOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
    void CompleteOrder(int id);
    int NumberOfOrders {get; }
    
}