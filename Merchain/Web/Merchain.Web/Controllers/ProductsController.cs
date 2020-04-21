namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Categories;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IReviewsService reviewsService;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IReviewsService reviewsService,
            ILogger<ProductsController> logger)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.reviewsService = reviewsService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index(int? page = 1, int? categoryId = null)
        {
            IEnumerable<ProductDefaultViewModel> products;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = this.productsService.GetProductsByCategory(categoryId)
                    .AsQueryable().To<ProductDefaultViewModel>();
            }
            else
            {
                products = await this.productsService.GetAllAsync<ProductDefaultViewModel>();
            }

            var categories = this.categoriesService.GetAll<CategoryDefaultViewModel>();

            var productsCount = products.Count();
            var pageSize = 12;

            products = products.Skip(((int)page - 1) * pageSize).Take(pageSize);

            var maxPage = (productsCount / pageSize) + (productsCount % pageSize == 0 ? 0 : 1);

            var viewModel = new ProductsIndexViewModel()
            {
                Categories = categories,
                Products = products,
                CurrentCategoryId = categoryId,
                CurrentPage = (int)page,
                MaxPage = maxPage,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productsService.GetByIdAsync((int)id);
            var relatedProducts = await this.productsService.GetAllAsync<ProductDefaultViewModel>();
            var reviewsCount = this.reviewsService.GetProductReviewsCount((int)id);
            var avgStars = this.reviewsService.AvgProductStars((int)id);

            var viewModel = new DetailsPageViewModel()
            {
                Product = product,
                ReviewsCount = reviewsCount,
                AvgStars = avgStars,
                RelatedProducts = relatedProducts.Take(5),
            };

            return this.View(viewModel);
        }

        public IActionResult WishList()
        {
            var wishList = SessionExtension.Get<List<LikedProduct>>(this.HttpContext.Session, SessionConstants.WishList);

            return this.View(wishList);
        }

        [HttpGet]
        public async Task<IActionResult> RefreshProducts(int skip, int? categoryId = -1, int? minPrice = 0, int? maxPrice = 9999)
        {
            IEnumerable<ProductDefaultViewModel> products;

            if (categoryId == null || categoryId == -1)
            {
                products = await this.productsService.GetAllAsync<ProductDefaultViewModel>();
            }
            else
            {
                products = this.productsService.GetProductsByCategory(categoryId)
                    .AsQueryable()
                    .To<ProductDefaultViewModel>();
            }

            products = products
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Skip(skip)
                .Take(6);

            return this.PartialView("/Views/Products/Partials/_ProductsIndex.cshtml", products);
        }

        public async Task<IActionResult> AddToWishList(int id)
        {
            try
            {
                await this.productsService.AddProductToWishList(this.HttpContext.Session, id);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not add product to the wish list.\n-{ex.Message}");
            }

            return new StatusCodeResult(200);
        }

        public IActionResult RemoveFromWishList(int id)
        {
            try
            {
                this.productsService.RemoveProductFromWishList(this.HttpContext.Session, id);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not remove product from the wish list.\n-{ex.Message}");
            }

            return new StatusCodeResult(200);
        }
    }
}
