using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    public IActionResult Index()
    {
        var users = _serviceManager.AuthService.GetAllUsers(false);
        return View(users);
    }
    
    public IActionResult Create()
    {
        var roles = new HashSet<string>(_serviceManager.AuthService.Roles.Select(x => x.Name));
        return View(new UserDtoForInsertion(){ Roles = roles});
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] UserDtoForInsertion userDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthService.CreateUser(userDto);
            TempData["success"] = $"{userDto.UserName} has been created.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    public async Task<IActionResult> Update([FromRoute(Name = "id")]string id)
    {
        var allRoles = _serviceManager.AuthService.GetAllRoles(false).Select(r => r.Name).ToHashSet();
        var user = await _serviceManager.AuthService.GetUserForUpdate(id, false);
        user.AllRoles = allRoles;
        user.Roles = allRoles;
        return View(user);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UserDtoForUpdate userDto)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthService.UpdateUser(userDto);
            TempData["success"] = $"{userDto.UserName} has been updated.";
            return RedirectToAction("Index");
        }
        else
        {
            var allRoles = _serviceManager.AuthService.GetAllRoles(false).Select(r => r.Name).ToHashSet();
            userDto.AllRoles = allRoles;
            userDto.Roles = allRoles;

            return View(userDto);
        }
    }
    
    public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")]string id)
    {
        var user = await _serviceManager.AuthService.GetUser(id, false);
        return View(new ResetPasswordDto()
        {
            Id = id,
            Email = user.Email
        });    
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (ModelState.IsValid)
        {
            var result =  await _serviceManager.AuthService.ResetPassword(resetPasswordDto);
            TempData["info"] = "The user pasword has been reset.";
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }
        else
        {

            return View();
        }
    }
    
    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")]string id)
    {
        _serviceManager.AuthService.DeleteUser(id);
        TempData["danger"] = "The user has been removed.";
        return RedirectToAction("Index");
    }
}