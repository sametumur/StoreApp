using Repositories.Contracts;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;

    public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _context = context;
        _categoryRepository = categoryRepository;
        _orderRepository = orderRepository;
    }

    public IProductRepository Products => _productRepository;
    public ICategoryRepository Category => _categoryRepository;
    public IOrderRepository Order => _orderRepository;

    public void Save()
    {
        _context.SaveChanges();
    }
}