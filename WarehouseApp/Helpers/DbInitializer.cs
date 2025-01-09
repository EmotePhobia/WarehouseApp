using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WarehouseApp.Models;
namespace WarehouseApp.Helpers
{
    public class DbInitializer
    {
        public static void SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            var adminEmail = "admin@admin.com";
            var adminPassword = "ZAQ!2wsx";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                var adminUser = new Customer {UserName = adminEmail, Email = adminEmail, FirstName = "Admin", LastName = "Admin" };
                var result = userManager.CreateAsync(adminUser, adminPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }
    }
}
