
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Models;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper: TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }
    
    [HtmlAttributeName("page-model")]
    public Pagination Pagination { get; set; }
    
    [HtmlAttributeName("page-action")]
    public string? PageAction { get; set; }
    
    [HtmlAttributeName("page-controller")]
    public string? PageController { get; set; }
    
    [HtmlAttributeName("page-area")]
    public string? PageArea { get; set; }
    
    public bool PageClassesEnabled { get; set; } = false;
    public string PageClass { get; set; } = String.Empty;
    public string PageClassNormal { get; set; } = String.Empty;
    public string PageClassSelected { get; set; } = String.Empty;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is not null && Pagination is not null)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("nav");
        
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination justify-content-center");
    
            // Previous Button
            var prevLi = new TagBuilder("li");
            prevLi.AddCssClass("page-item");
            if (Pagination.CurrentPage == 1)
                prevLi.AddCssClass("disabled");
    
            var prevA = new TagBuilder("a");
            prevA.AddCssClass("page-link");
            
            if (PageClassesEnabled)
            {
                if (!string.IsNullOrEmpty(PageClass))
                    prevA.AddCssClass(PageClass);
                if (!string.IsNullOrEmpty(PageClassNormal))
                    prevA.AddCssClass(PageClassNormal);
            }
            
            prevA.Attributes["href"] = Pagination.CurrentPage == 1 ? "#" : 
                GenerateUrl(urlHelper, Pagination.CurrentPage - 1);
            prevA.Attributes["tabindex"] = "-1";
            prevA.Attributes["aria-disabled"] = "true";
            prevA.InnerHtml.Append("Previous");
            prevLi.InnerHtml.AppendHtml(prevA);
            ul.InnerHtml.AppendHtml(prevLi);
    
            // Page Numbers
            for (int i = 1; i <= Pagination.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("page-item");
                if (i == Pagination.CurrentPage)
                    li.AddCssClass("active");
    
                TagBuilder a = new TagBuilder("a");
                a.AddCssClass("page-link");
                
                if (PageClassesEnabled)
                {
                    if (!string.IsNullOrEmpty(PageClass))
                        a.AddCssClass(PageClass);
                        
                    if (i == Pagination.CurrentPage && !string.IsNullOrEmpty(PageClassSelected))
                        a.AddCssClass(PageClassSelected);
                    else if (!string.IsNullOrEmpty(PageClassNormal))
                        a.AddCssClass(PageClassNormal);
                }
                
                a.Attributes["href"] = GenerateUrl(urlHelper, i);
                a.InnerHtml.Append(i.ToString());
            
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
    
            // Next Button
            var nextLi = new TagBuilder("li"); 
            nextLi.AddCssClass("page-item");
            if (Pagination.CurrentPage == Pagination.TotalPages)
                nextLi.AddCssClass("disabled");

            var nextA = new TagBuilder("a");
            nextA.AddCssClass("page-link");
            
            if (PageClassesEnabled)
            {
                if (!string.IsNullOrEmpty(PageClass))
                    nextA.AddCssClass(PageClass);
                if (!string.IsNullOrEmpty(PageClassNormal))
                    nextA.AddCssClass(PageClassNormal);
            }
            
            nextA.Attributes["href"] = Pagination.CurrentPage == Pagination.TotalPages ? "#" :
                GenerateUrl(urlHelper, Pagination.CurrentPage + 1);
            nextA.InnerHtml.Append("Next");
            nextLi.InnerHtml.AppendHtml(nextA);
            ul.InnerHtml.AppendHtml(nextLi);
    
            result.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(result);
        }
    }
    
    private string GenerateUrl(IUrlHelper urlHelper, int page)
    {
        // Mevcut route değerlerini al
        var routeValues = new Dictionary<string, object?>();
        
        // Sayfa numarasını ekle
        routeValues["page"] = page;
        
        // Area bilgisini belirle
        var area = !string.IsNullOrEmpty(PageArea) ? PageArea : 
                   ViewContext?.RouteData.Values["area"]?.ToString();
        
        // Controller bilgisini belirle
        var controller = !string.IsNullOrEmpty(PageController) ? PageController : 
                        ViewContext?.RouteData.Values["controller"]?.ToString();
        
        // Action bilgisini belirle
        var action = !string.IsNullOrEmpty(PageAction) ? PageAction : 
                    ViewContext?.RouteData.Values["action"]?.ToString();
        
        // Area varsa area bilgisini ekle
        if (!string.IsNullOrEmpty(area))
        {
            routeValues["area"] = area;
        }
        
        return urlHelper.Action(action, controller, routeValues);
    }
}