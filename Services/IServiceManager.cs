using Services.Contracts;

namespace Services;

public interface IServiceManager
{
    IProductService ProductService { get; }
    ICategoryService CategoryService { get; }
    IOrderService OrderService { get; }
    IAuthService AuthService { get; }
   
}