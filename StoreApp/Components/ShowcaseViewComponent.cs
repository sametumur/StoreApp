using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Services;

namespace StoreApp.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ShowcaseViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public ViewViewComponentResult Invoke(string page = "default")
        {
            var products = _manager.ProductService.GetShowCaseProducts(false);
            return page.Equals("default")
                ?  View(products)
                :  View("List",products);
        }
    }
}