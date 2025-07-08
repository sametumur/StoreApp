using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.RequestParameters;
using Repositories;
using Repositories.Contracts;
using Services;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }


    [Route("product")]
    [Route("product/page/{page:int?}")]
    [Route("product/category/{categoryId:int?}/page/{page:int?}")]
    public IActionResult Index(ProductRequestParameters parameters)
    {
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
    
    public IActionResult Get([FromRoute(Name = "id")]int id)
    {
        
        var product = _serviceManager.ProductService.GetProduct(id, false);
        ViewData["Title"] = product.Name;
        return View(product);
    }
}