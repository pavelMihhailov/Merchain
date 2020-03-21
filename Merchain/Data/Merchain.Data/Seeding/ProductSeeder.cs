namespace Merchain.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedProducts(dbContext);
        }

        private static async Task SeedProducts(ApplicationDbContext dbContext)
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Name = "Blue T-Shirt",
                    Description = "Description Test",
                    ImageUrl = "https://res.cloudinary.com/pavelscloud/image/upload/v1584218830/quvhqnlpxnt2w7qgdwps.jpg",
                    Price = 32.99M,
                },
                new Product()
                {
                    Name = "Black T-Shirt",
                    Description = "Description 2 Test",
                    ImageUrl = "https://res.cloudinary.com/pavelscloud/image/upload/v1584125769/w2f3ojulqjudhpasuc83.jpg",
                    Price = 30.99M,
                },
                new Product()
                {
                    Name = "Grey T-Shirt",
                    Description = "Description 3 Test",
                    ImageUrl = "https://res.cloudinary.com/pavelscloud/image/upload/v1584134402/auwchveyqpffynnyw2sm.jpg",
                    Price = 29.99M,
                },
            };

            foreach (var product in products)
            {
                var productExists = dbContext.Products.Any(x => x.Name == product.Name);
                if (!productExists)
                {
                    await dbContext.Products.AddAsync(product);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
