using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login([FromQuery(Name="ReturnUrl")] string returnUrl = "/")
    {
        return View(new LoginViewModel(){ ReturnUrl = returnUrl});
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Name);
            if(user is not null)
            {
                await _signInManager.SignOutAsync();
                if((await _signInManager.PasswordSignInAsync(user, model.Password,false,false)).Succeeded)
                {
                    return Redirect(model?.ReturnUrl ?? "/");
                }
            }
            ModelState.AddModelError("Error","Invalid username or password.");
        }
        return View();
    }
    
    public async Task<IActionResult> Logout([FromQuery(Name="ReturnUrl")] string returnUrl = "/")
    {
        await _signInManager.SignOutAsync();
        if (IsUnsafeReturnUrl(returnUrl))
        {
            return Redirect("/");
        }
    
        return Redirect(returnUrl);

    }
    
    private bool IsUnsafeReturnUrl(string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
            return false;
        
        var unsafePatterns = new[]
        {
            "/account/profile",
            "/admin",
            "/dashboard"
        };
    
        return unsafePatterns.Any(pattern => 
            returnUrl.StartsWith(pattern, StringComparison.OrdinalIgnoreCase));
    }

    
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        var user = new IdentityUser()
        {
            UserName = dto.UserName,
            Email = dto.Email,
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if(result.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if(roleResult.Succeeded)
            {
                return RedirectToAction("Login", new {ReturnUrl = "/"});
            }
        }
        else
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View();
    }
    
    public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returUrl)
    {
        return View();
    }
    
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        
        ViewBag.Name = user.UserName;
        ViewBag.Email = user.Email;
        ViewBag.PhoneNumber = user.PhoneNumber;

        return View();
    }
}