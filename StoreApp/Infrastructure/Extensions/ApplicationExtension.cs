using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions;

public static class ApplicationExtension
{
    public static void ConfigureAndCheckMigration(this IApplicationBuilder applicationBuilder)
    {
        RepositoryContext context = applicationBuilder
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    public static void ConfigureLocalization(this WebApplication webApplication)
    {
        webApplication.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures("tr-TR", "en-US").AddSupportedUICultures("tr-TR", "en-US").SetDefaultCulture("tr-TR");
        });
    }

    public static async void ConfigureDefaultAdminUser(this IApplicationBuilder applicationBuilder)
    {
        
        const string adminUser = "Admin";
        const string adminPassword = "Admin+123456";

        // UserManager
        UserManager<IdentityUser> userManager = applicationBuilder 
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
            
        // RoleManager
        RoleManager<IdentityRole> roleManager = applicationBuilder 
            .ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        IdentityUser user = await userManager.FindByNameAsync(adminUser);
        if(user is null)
        {
            user = new IdentityUser()
            {
                Email = "admin@localhost",
                PhoneNumber = "5061112233",
                UserName = adminUser,
            };

            var result = await userManager.CreateAsync(user,adminPassword);
                
            if(!result.Succeeded)
                throw new Exception("Admin user could not been created.");

            var roleResult = await userManager.AddToRolesAsync(user,
                roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToList()
            );

            if(!roleResult.Succeeded)
                throw new Exception("System have problems with role defination for admin.");
        }
    }
}