using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IServiceManager _serviceManager;

    public OrderController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index()
    {
        var orders = _serviceManager.OrderService.GetAllOrders;
        return View(orders);
    }
    
    public IActionResult Detail([FromRoute(Name = "id")]int id)
    {
        var order = _serviceManager.OrderService.GetOrder(id, false);
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete([FromRoute(Name = "id")]int id)
    {
        _serviceManager.OrderService.CompleteOrder(id);
        return RedirectToAction("Index");
    }
}