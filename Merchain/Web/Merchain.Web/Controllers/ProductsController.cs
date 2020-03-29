namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Categories;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAll<CategoryDefaultViewModel>();
            var products = this.productsService.GetAll<ProductDefaultViewModel>();

            var minPrice = (int)Math.Ceiling(products.Min(x => x.Price));
            var maxPrice = (int)Math.Ceiling(products.Max(x => x.Price));

            var viewModel = new ProductsIndexViewModel()
            {
                Categories = categories,
                Products = products.Take(12),
                MinPrice = minPrice,
                MaxPrice = maxPrice,
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

            var viewModel = new DetailsPageViewModel()
            {
                Product = product,
                RelatedProducts = relatedProducts.Take(5),
            };

            return this.View(viewModel);
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
    }
}