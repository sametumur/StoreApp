using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}