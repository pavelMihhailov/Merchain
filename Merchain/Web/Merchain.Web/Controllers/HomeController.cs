namespace Merchain.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Merchain.Common;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels;
    using Merchain.Web.ViewModels.Home;
    using Merchain.Web.ViewModels.Products;
    using Merchain.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public HomeController(
            IProductsService productsService,
            ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var latestProducts = this.productsService
                .GetAllDescending<ProductDefaultViewModel>(x => x.CreatedOn)
                .Take(5);

            var bestSellingProducts = this.productsService
                .GetAllDescending<ProductDefaultViewModel>(x => x.Orders)
                .Take(5);

            var categories = this.categoriesService.GetAll<CategoryViewModel>();

            var viewModel = new IndexViewModel()
            {
                LatestProducts = latestProducts,
                BestSellingProducts = bestSellingProducts,
                Categories = categories,
            };

            this.ViewData[ViewDataConstants.SucccessMessage] = this.TempData[ViewDataConstants.SucccessMessage];
            this.ViewData[ViewDataConstants.ErrorMessage] = this.TempData[ViewDataConstants.ErrorMessage];

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult ListProductsBy(int? categoryId)
        {
            if (categoryId == null)
            {
                return new EmptyResult();
            }

            var products = this.productsService.GetProductsByCategory(categoryId)
                .AsQueryable()
                .To<ProductDefaultViewModel>();

            return this.PartialView("/Views/Products/Partials/_ListProducts.cshtml", products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
