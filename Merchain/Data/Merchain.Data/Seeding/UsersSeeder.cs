namespace Merchain.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUsersAsync(userManager);
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync("pavel");
            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser()
                    {
                        UserName = "pavel",
                        Email = "pavel.nm@abv.bg",
                        EmailConfirmed = true,
                    },
                    password: "pavel98");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
