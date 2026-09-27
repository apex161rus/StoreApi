using Api.Common;
using Microsoft.AspNetCore.Identity;

namespace Api.Extension
{
    public static class RolelnitializerServiceExtension
    {
        public static async Task InitializeRoleAsync(
            this IServiceProvider serviceProvider
        )
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = 
            scope.
            ServiceProvider.

            GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in SharedData.Roles.AllRoles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}