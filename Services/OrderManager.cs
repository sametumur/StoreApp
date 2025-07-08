using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class OrderManager : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    public OrderManager(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public IQueryable<Order> GetAllOrders => _repositoryManager.Order.GetAllOrders;
    
    public Order? GetOrder(int id, bool trackChanges)
    {
        return _repositoryManager.Order.GetOrder(id, trackChanges);
    }
    

    public void CreateOrder(Order order)
    {
        _repositoryManager.Order.CreateOrder(order);
        _repositoryManager.Save();
    }

    public void UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public void DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public void CompleteOrder(int id)
    {
        _repositoryManager.Order.CompleteOrder(id);
        _repositoryManager.Save();
    }

    public int NumberOfOrders => _repositoryManager.Order.NumberOfOrders;
}