using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Controllers;

public class OrderController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly Cart _cart;

    public OrderController(IServiceManager serviceManager, Cart cart)
    {
        _serviceManager = serviceManager;
        _cart = cart;
    }
    
    [Authorize (Roles = "User")]
    public ViewResult Checkout()
    {
        return View(new Order());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout([FromForm] Order order)
    {
        if (_cart.CartLines.Count() == 0)
        {
            ModelState.AddModelError("", "Cart is empty");
        }

        if (ModelState.IsValid)
        {
            order.CartLines = _cart.CartLines.ToArray();
            order.TotalPrice = _cart.TotalPrice;
            _serviceManager.OrderService.CreateOrder(order);
            _cart.Clear();
            return RedirectToPage("/CheckoutSuccess", new {orderId = order.Id});
        }
        
        return View(order);
    }
}