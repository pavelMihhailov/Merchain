namespace Merchain.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.CloudinaryService;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Colors;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Color> colorRepository;
        private readonly IRepository<ProductColor> productsColorsRepo;
        private readonly IRepository<ProductCategory> productsCategoriesRepo;
        private readonly CloudinaryService cloudinaryService;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Color> colorRepository,
            IRepository<ProductColor> productsColorsRepo,
            IRepository<ProductCategory> productsCategoriesRepo,
            CloudinaryService cloudinaryService,
            ILogger<ProductsService> logger)
        {
            this.productsRepository = productsRepository;
            this.categoryRepository = categoryRepository;
            this.colorRepository = colorRepository;
            this.productsColorsRepo = productsColorsRepo;
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

        public T GetById<T>(int id)
        {
            var product = this.productsRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
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
            IEnumerable<IFormFile> images,
            IEnumerable<int> categoryIds,
            IEnumerable<int> colorIds)
        {
            try
            {
                var product = new Product()
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    ProductsCategories = new List<ProductCategory>(),
                };

                var imagesUrls = new StringBuilder();

                foreach (var image in images)
                {
                    var imageUrl = await this.cloudinaryService.UploadImage(image);

                    imagesUrls.Append((imagesUrls.Length > 0 ? ";" : string.Empty) + imageUrl);
                }

                product.ImagesUrls = imagesUrls.ToString();

                await this.AddCategoriesToProduct(categoryIds, product);
                await this.AddColorsToProduct(colorIds, product);

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
            if (product == null)
            {
                return Task.CompletedTask;
            }

            this.productsRepository.Update(product);

            await this.productsRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> Edit(Product product, IEnumerable<IFormFile> addedImages, IEnumerable<int> categoryIds, IEnumerable<int> colorIds)
        {
            if (categoryIds != null)
            {
                await this.UpdateProductCategories(categoryIds, product);
            }

            if (colorIds != null)
            {
                await this.UpdateProductColors(colorIds, product);
            }

            if (addedImages != null)
            {
                var imagesUrls = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(product.ImagesUrls))
                {
                    imagesUrls.Append(product.ImagesUrls);
                }

                foreach (var image in addedImages)
                {
                    var imageUrl = await this.cloudinaryService.UploadImage(image);

                    if (!string.IsNullOrWhiteSpace(imagesUrls.ToString()))
                    {
                        imagesUrls.Append(";");
                    }

                    imagesUrls.Append(imageUrl);
                }

                product.ImagesUrls = imagesUrls.ToString();
            }

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

        public IQueryable<ColorViewModel> GetColorsOfProduct(int productId)
        {
            IQueryable<ColorViewModel> colors = this.productsColorsRepo.All()
                .Where(x => x.ProductId == productId)
                .Select(s => s.Color)
                .To<ColorViewModel>();

            return colors;
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

        public void RemoveProductFromWishList(ISession session, int id)
        {
            var wishList = SessionExtension.Get<List<LikedProduct>>(session, SessionConstants.WishList);

            if (wishList != null)
            {
                int productListId = this.GetProductListId(session, id);

                wishList.RemoveAt(productListId);

                SessionExtension.Set(session, SessionConstants.WishList, wishList);
            }
        }

        private async Task AddCategoriesToProduct(IEnumerable<int> categoryIds, Product product)
        {
            foreach (int categoryId in categoryIds)
            {
                Category category = await this.categoryRepository.GetById(categoryId);

                bool isCategoryAlreadyAdded = this.productsCategoriesRepo.All()
                    .Any(x => x.CategoryId == categoryId && x.ProductId == product.Id);

                if (category != null && !isCategoryAlreadyAdded)
                {
                    product.ProductsCategories.Add(
                        new ProductCategory { Category = category, Product = product });
                }
            }
        }

        private async Task AddColorsToProduct(IEnumerable<int> colorIds, Product product)
        {
            foreach (int colorId in colorIds)
            {
                Color color = await this.colorRepository.GetById(colorId);

                var isColorAlreadyAdded = this.productsColorsRepo.All()
                    .Any(x => x.ColorId == colorId && x.ProductId == product.Id);

                if (color != null && !isColorAlreadyAdded)
                {
                    product.ProductsColors.Add(
                        new ProductColor { Color = color, Product = product });
                }
            }
        }

        private async Task UpdateProductCategories(IEnumerable<int> categoryIds, Product product)
        {
            var currentCategories = await this.productsCategoriesRepo.All()
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            if (categoryIds == null)
            {
                categoryIds = new List<int>();
            }

            // Remove unselected categories if any
            foreach (var currentCategory in currentCategories)
            {
                if (!categoryIds.Contains(currentCategory.CategoryId))
                {
                    this.productsCategoriesRepo.Delete(currentCategory);
                }
            }

            await this.productsCategoriesRepo.SaveChangesAsync();

            await this.AddCategoriesToProduct(categoryIds, product);
        }

        private async Task UpdateProductColors(IEnumerable<int> colorIds, Product product)
        {
            var currentColors = await this.productsColorsRepo.All()
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            if (colorIds == null)
            {
                colorIds = new List<int>();
            }

            // Remove unselected categories if any
            foreach (var currentColor in currentColors)
            {
                if (!colorIds.Contains(currentColor.ColorId))
                {
                    this.productsColorsRepo.Delete(currentColor);
                }
            }

            await this.productsColorsRepo.SaveChangesAsync();

            await this.AddColorsToProduct(colorIds, product);
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

        private int GetProductListId(ISession session, int id)
        {
            var wishList = SessionExtension.Get<List<LikedProduct>>(session, SessionConstants.WishList);

            for (int i = 0; i < wishList.Count; i++)
            {
                if (wishList[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
