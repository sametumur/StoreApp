using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("td", Attributes ="user-role")]
public class UserRoleTagHelper : TagHelper
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    [HtmlAttributeName("user-name")]
    public String? UserName { get; set; }
        
    public UserRoleTagHelper(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var user = await _userManager.FindByNameAsync(UserName);
        var roles = _roleManager.Roles.ToList().Select(r => r.Name);
        var userRoles = new List<string>();

        foreach (var role in roles)
        {
            if(await _userManager.IsInRoleAsync(user,role))
            {
                userRoles.Add(role);
            }
        }

        output.Content.SetContent(string.Join(" - ", userRoles));
    }

}