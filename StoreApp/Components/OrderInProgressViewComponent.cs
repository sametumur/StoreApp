using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Components
{
    public class OrderInProgressViewComponent : ViewComponent
    {
        
        private readonly IServiceManager _manager;

        public OrderInProgressViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke()
        {
            return _manager
                .OrderService
                .NumberOfOrders
                .ToString();
        }
    }
}