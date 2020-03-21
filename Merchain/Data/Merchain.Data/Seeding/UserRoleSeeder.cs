namespace Merchain.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UserRoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserRoleAsync(dbContext, roleManager, userManager, "pavel", GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserRoleAsync(
            ApplicationDbContext dbContext,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            string username,
            string roleName)
        {
            var user = await userManager.FindByNameAsync(username);
            var role = await roleManager.FindByNameAsync(roleName);

            bool exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = role.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}