
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages;

public class CartModel : PageModel
{
    
    private readonly IServiceManager _serviceManager;
    public Cart Cart { get; set; }

    public CartModel(IServiceManager serviceManager, Cart cartService)
    {
        _serviceManager = serviceManager;
        Cart = cartService;
    }
    
    public string ReturnUrl { get; set; } = "/";
    
    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public IActionResult OnPost(int productId, string returnUrl)
    {
      Product? product =  _serviceManager.ProductService.GetProduct(productId, false);
      if (product is not null)
      {
          Cart.AddProduct(product, 1);
      }
      return RedirectToPage(new { returnUrl = returnUrl });

    }

    
    public IActionResult OnPostRemove(int productId, string returnUrl)
    {
        Cart.RemoveProduct(Cart.CartLines.First(cl => cl.Product.Id.Equals(productId)).Product);
        return Page();
    }
}