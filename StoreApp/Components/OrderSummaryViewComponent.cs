using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Components;

public class OrderSummaryViewComponent : ViewComponent
{
    private readonly IServiceManager _serviceManager;

    public OrderSummaryViewComponent(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }


    public string? Invoke()
    {
        return _serviceManager.OrderService.GetAllOrders.Count().ToString();
    }
}