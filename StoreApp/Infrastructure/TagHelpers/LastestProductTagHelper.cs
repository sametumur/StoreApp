using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "latest-products")]
public class LastestProductTagHelper : TagHelper
{
    private readonly IServiceManager _serviceManager;
    [HtmlAttributeName("count")]
    public int Number { get; set; }

    public LastestProductTagHelper(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var article = new TagBuilder("article");
        article.Attributes.Add("class", "card-group-item");
    
        var header = new TagBuilder("header"); 
        header.Attributes.Add("class", "card-header");
    
        var h6 = new TagBuilder("h6");
        h6.Attributes.Add("class", "title");
    
        var i = new TagBuilder("i");
        i.Attributes.Add("class", "fa-solid fa-box");
    
        h6.InnerHtml.AppendHtml(i);
        h6.InnerHtml.AppendHtml(" Latest Products");
        header.InnerHtml.AppendHtml(h6);
    
        var filterContent = new TagBuilder("div");
        filterContent.Attributes.Add("class", "filter-content");
    
        var cardBody = new TagBuilder("div");
        cardBody.Attributes.Add("class", "card-body");
    
        var formRow = new TagBuilder("div");
        formRow.Attributes.Add("class", "form-row");
    
        var ul = new TagBuilder("ul");
        var products = _serviceManager.ProductService.GetLatestProducts(Number, false);
        foreach (Product product in products)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            a.AddCssClass("link-secondary");
            a.Attributes.Add("href", $"/Product/Get/{product.Id}");
            if (product.Name != null) a.InnerHtml.AppendHtml(product.Name);
            li.InnerHtml.AppendHtml(a);
            ul.InnerHtml.AppendHtml(li);
        }
    
        formRow.InnerHtml.AppendHtml(ul);
        cardBody.InnerHtml.AppendHtml(formRow);
        filterContent.InnerHtml.AppendHtml(cardBody);
    
        article.InnerHtml.AppendHtml(header);
        article.InnerHtml.AppendHtml(filterContent);
    
        output.Content.AppendHtml(article);
    }
}