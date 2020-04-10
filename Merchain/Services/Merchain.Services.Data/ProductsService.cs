namespace Merchain.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;
    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.CloudinaryService;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IRepository<ProductCategory> productsCategoriesRepo;
        private readonly CloudinaryService cloudinaryService;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IRepository<ProductCategory> productsCategoriesRepo,
            CloudinaryService cloudinaryService,
            ILogger<ProductsService> logger)
        {
            this.productsRepository = productsRepository;
            this.categoryRepository = categoryRepository;
            this.productsCategoriesRepo = productsCategoriesRepo;
            this.cloudinaryService = cloudinaryService;
            this.logger = logger;
        }

        public int GetCount()
        {
            return this.productsRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.productsRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllDescending<T>(Expression<Func<Product, object>> orderBy)
        {
            return this.productsRepository.All()
                .OrderByDescending(orderBy)
                .To<T>()
                .ToList();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await this.productsRepository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await this.productsRepository.All().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.productsRepository.All().To<T>().ToListAsync();
        }

        public async Task<Task> AddProductAsync(Product product)
        {
            product.CreatedOn = DateTime.UtcNow;

            await this.productsRepository.AddAsync(product);

            await this.productsRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task CreateProduct(
            string name,
            string description,
            decimal price,
            IFormFile image,
            IEnumerable<int> categoryIds)
        {
            try
            {
                var product = new Product()
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Likes = 0,
                    ImageUrl = await this.cloudinaryService.UploadImage(image),
                    ProductsCategories = new List<ProductCategory>(),
                };

                await this.AddCategoriesToProduct(categoryIds, product);

                await this.AddProductAsync(product);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not create the product.\n{ex.Message}");
                throw;
            }
        }

        public async Task<Task> Edit(Product product)
        {
            this.productsRepository.Update(product);

            await this.productsRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> Delete(Product product)
        {
            this.productsRepository.Delete(product);

            await this.productsRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public IEnumerable<Product> GetProductsByCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return new List<Product>();
            }

            var products = this.productsCategoriesRepo.All()
                .Where(x => x.Category.Id == categoryId)
                .Select(cp => cp.Product)
                .AsEnumerable();

            return products;
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return new List<Product>();
            }

            var products = this.productsCategoriesRepo.All()
                .Where(x => x.Category.Title == categoryName)
                .Select(cp => cp.Product)
                .AsEnumerable();

            return products;
        }

        public async Task AddProductToWishList(ISession session, int id)
        {
            var product = await this.GetByIdAsync(id);

            if (product != null)
            {
                var wishList = SessionExtension.Get<List<LikedProduct>>(session, SessionConstants.WishList);

                this.SetWishListSession(session, id, product, wishList);
            }
        }

        private async Task AddCategoriesToProduct(IEnumerable<int> categoryIds, Product product)
        {
            foreach (int categoryId in categoryIds)
            {
                Category category = await this.categoryRepository.GetById(categoryId);

                if (category != null)
                {
                    product.ProductsCategories.Add(
                        new ProductCategory { Category = category, Product = product });
                }
            }
        }

        private void SetWishListSession(ISession session, int id, Product product, IEnumerable<LikedProduct> wishList)
        {
            if (wishList == null)
            {
                var likedPoducts = new List<LikedProduct>();
                likedPoducts.Add(new LikedProduct { Product = product });

                SessionExtension.Set(session, SessionConstants.WishList, likedPoducts);
            }
            else
            {
                var likedProduct = wishList.FirstOrDefault(x => x.Product.Id == id);

                if (likedProduct == null)
                {
                    var wishListItem = new List<LikedProduct>() { new LikedProduct { Product = product } };
                    wishList = wishList.Concat(wishListItem);

                    SessionExtension.Set(session, SessionConstants.WishList, wishList);
                }
            }
        }
    }
}
