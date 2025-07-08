using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Order> GetAllOrders =>  _context.Orders
        .Include(o => o.CartLines)!
        .ThenInclude(cl => cl.Product)
        .OrderBy(o => o.Shipped)
        .ThenByDescending<Order, int>(o => o.Id);
    
    public Order? GetOrder(int id, bool trackChanges)
    {
       return _context.Orders
           .Include(o => o.CartLines)!
           .ThenInclude(cl => cl.Product)
           .Where(o => o.Id == id)
           .ToList().FirstOrDefault();
    }

    public void CreateOrder(Order order)
    {
        _context.AttachRange(order.CartLines.Select(l => l.Product));
        if (order.Id == 0)
            _context.Orders.Add(order);
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
        var order = FindByCondition(o => o.Id == id, true);
        if (order == null)
        {
            throw new Exception("Order not found");       
        }
        order!.Shipped = true;
    }

    public int NumberOfOrders => _context.Orders.Count(o => !o.Shipped);
}