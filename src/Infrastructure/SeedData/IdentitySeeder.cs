using Domain.AggregateRoots;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SeedData;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!"; // ToDo: Change it for something safer
        string adminRole = "Admin";

        // Create role if it doesn't exist
        if (!await roleManager.RoleExistsAsync(adminRole))
            await roleManager.CreateAsync(new Role(adminRole));

        // Check if the admin user exists
        var existingUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingUser == null)
        {
            var user = new User(adminEmail, adminEmail);

            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, adminRole);
            else
            {
                // Optional: handle errors
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error creating admin user: {errors}");
            }
        }
    }
}
