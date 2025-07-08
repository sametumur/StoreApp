using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services;
using StoreApp.Models;


namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index(ProductRequestParameters parameters)
    {
        ViewData["CurrentArea"] = "Admin";
        ViewData["CurrentController"] = "Product";
        var products = _serviceManager.ProductService.GetAllProductsWithDetails(parameters);
        var pagination = new Pagination()
        {
            CurrentPage = parameters.Page,
            PageSize = parameters.PageSize,
            TotalItems = _serviceManager.ProductService.GetAllProducts(false).Count()
        };
        return View(new ProductListViewModel()
        {
            Products = products,
            Pagination = pagination
        });
    }

    public IActionResult Create()
    {
        TempData["info"] = "Please fill the form.";
        ViewBag.Categories = this.GetCategories();
        return View();
    }

    private SelectList GetCategories()
    {
        return ViewBag.Categories = new SelectList(
            _serviceManager.CategoryService.GetAllCategories(false),
            "Id",
            "Name",
            "Select Category:"
        );
    }
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            productDto.ImageUrl = string.Concat("/images/", file.FileName);
            _serviceManager.ProductService.CreateProduct(productDto);
            TempData["success"] = $"{productDto.Name} has been created.";
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    
    [Route("Admin/Product/Update/{id:int}")]
    public IActionResult Update([FromRoute(Name = "id")]int id)
    {
        ViewBag.Categories = this.GetCategories();
        var product = _serviceManager.ProductService.GetProductForUpdate(id, false);
        return View(product);
    }
    
    [HttpPost]
    [Route("Admin/Product/Update/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id,
        ProductDtoForUpdate productDto, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (file != null && file.Length > 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = string.Concat("/images/", file.FileName); 
            }
           
            _serviceManager.ProductService.UpdateProduct(productDto);
            TempData["success"] = $"{productDto.Name} has been updated.";
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        else
        {
            return View();
        }
    }
    
    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")]int id)
    {
        _serviceManager.ProductService.DeleteProduct(id);
        TempData["danger"] = "The product has been removed.";
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }
}