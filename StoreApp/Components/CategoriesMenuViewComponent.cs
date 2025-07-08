using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Components;

public class CategoriesMenuViewComponent : ViewComponent
{
    private readonly IServiceManager _serviceManager;

    public CategoriesMenuViewComponent(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IViewComponentResult Invoke(string targetArea = "")
    {
        ViewBag.TargetArea = targetArea;
        var categories = _serviceManager.CategoryService.GetAllCategories(false);
        return View(categories);
    }
}