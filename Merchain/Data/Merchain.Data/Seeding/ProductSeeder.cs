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
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1584218830/quvhqnlpxnt2w7qgdwps.jpg",
                    Price = 32.99M,
                },
                new Product()
                {
                    Name = "Black T-Shirt",
                    Description = "Description 2 Test",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1584125769/w2f3ojulqjudhpasuc83.jpg",
                    Price = 30.99M,
                },
                new Product()
                {
                    Name = "Grey T-Shirt",
                    Description = "Description 3 Test",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1584134402/auwchveyqpffynnyw2sm.jpg",
                    Price = 29.99M,
                },
                new Product()
                {
                    Name = "Flamboyant Pink Top",
                    Description = "testtt",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585477918/x3qlmacrxuinedqinogf.jpg",
                    Price = 35.00M,
                },
                new Product()
                {
                    Name = "Flamboyant Black Top",
                    Description = "testttdassdadas",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585477962/irgxhefjqqbueqtgrkar.jpg",
                    Price = 89.99M,
                },
                new Product()
                {
                    Name = "Neshto Kato Roklq",
                    Description = "testdasdsdas",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478041/hmsenswit6sbbles6miu.jpg",
                    Price = 99.00M,
                },
                new Product()
                {
                    Name = "Different Top",
                    Description = "testdasdsdas",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478089/qrkmupcjcfnzsmfulfo0.jpg",
                    Price = 69.69M,
                },
                new Product()
                {
                    Name = "Black and White Stripes Dress",
                    Description = "nqma",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478120/hwsug70b4ppyosmc3sbc.jpg",
                    Price = 85.98M,
                },
                new Product()
                {
                    Name = "Flamboyant White Top",
                    Description = "adsasddas",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478161/swzsnfu7vtrxqo3pwj1q.jpg",
                    Price = 33.38M,
                },
                new Product()
                {
                    Name = "Stripes Top",
                    Description = "wqeqweqwew",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478195/obxscarn8qp5xxylamzl.jpg",
                    Price = 233.33M,
                },
                new Product()
                {
                    Name = "White Dress	",
                    Description = "etrewrw",
                    ImagesUrls = "https://res.cloudinary.com/pavelscloud/image/upload/v1585478259/x1howaoewadl2uxs8lgf.jpg",
                    Price = 233.93M,
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
