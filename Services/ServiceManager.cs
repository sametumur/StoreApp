using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IOrderService _orderService;
    private readonly IAuthService _authService;

    public ServiceManager(ICategoryService categoryService, IProductService productService, IOrderService orderService, IAuthService authService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _orderService = orderService;
        _authService = authService;
    }


    public IProductService ProductService => _productService;
    public ICategoryService CategoryService => _categoryService;
    public IOrderService OrderService => _orderService;
    public IAuthService AuthService => _authService;
}