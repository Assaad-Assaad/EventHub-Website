using EventHubWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

public class DataSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var roles = new[] { "Admin", "Manager", "Member", "Default" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            
               await roleManager.CreateAsync(new IdentityRole<int>(role));
            
        }
        
    }

    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        string firstName = "Admin";
        string lastName = "Admin";
        string email = "admin@eventhub.com";
        string password = "Admin@eventhub123";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new AppUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email 
            };

            await userManager.CreateAsync(user, password); 
            
            
            await userManager.AddToRoleAsync(user, "Admin");
           
        }
    }
}

