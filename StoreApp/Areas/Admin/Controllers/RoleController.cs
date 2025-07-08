using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly IServiceManager _serviceManager;

    public RoleController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    public IActionResult Index()
    {
        return View(_serviceManager.AuthService.Roles);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] RoleDtoForInsertion roleDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthService.CreateRole(roleDto);
            TempData["success"] = $"{roleDto.Name} has been created.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    public IActionResult Update([FromRoute(Name = "id")]string id)
    {

        var role = _serviceManager.AuthService.GetRoleForUpdate(id, false);
        return View(role);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(RoleDtoForUpdate roleDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthService.UpdateRole(roleDto);
            TempData["success"] = $"{roleDto.Name} has been updated.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")]string id)
    {
        _serviceManager.AuthService.DeleteRole(id);
        TempData["danger"] = "The role has been removed.";
        return RedirectToAction("Index");
    }
}