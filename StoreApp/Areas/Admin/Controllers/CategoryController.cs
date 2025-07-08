using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;


namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IServiceManager _serviceManager;

    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    public IActionResult Index()
    {
        var categories = _serviceManager.CategoryService.GetAllCategories(false);
        return View(categories);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] CategoryDtoForInsertion categoryDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.CategoryService.CreateCategory(categoryDto);
            TempData["success"] = $"{categoryDto.Name} has been created.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    public IActionResult Update([FromRoute(Name = "id")]int id)
    {
        
        var category = _serviceManager.CategoryService.GetCategoryForUpdate(id, false);
        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(CategoryDtoForUpdate categoryDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.CategoryService.UpdateCategory(categoryDto);
            TempData["success"] = $"{categoryDto.Name} has been created.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")]int id)
    {
        _serviceManager.CategoryService.DeleteCategory(id);
        TempData["danger"] = "The category has been removed.";
        return RedirectToAction("Index");
    }
}