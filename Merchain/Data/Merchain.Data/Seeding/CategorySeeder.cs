namespace Merchain.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            // Uncomment if you want initial seeding of categories
            // await SeedCategories(dbContext);
        }

        public async Task SeedCategories(ApplicationDbContext dbContext)
        {
            var categories = new List<string>()
            {
                "Мъжки Тениски",
                "Дамски Тениски",
                "Суитшърти",
            };

            foreach (var category in categories)
            {
                if (!dbContext.Categories.Any(x => x.Title == category))
                {
                    await dbContext.Categories.AddAsync(new Category() { Title = category });
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
