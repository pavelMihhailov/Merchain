namespace Merchain.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels;
    using Merchain.Web.ViewModels.Home;
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
                .GetAllDescending<ProductHomeViewModel>(x => x.CreatedOn)
                .Take(5);

            var bestSellingProducts = this.productsService
                .GetAllDescending<ProductHomeViewModel>(x => x.Orders)
                .Take(5);

            var categories = this.categoriesService.GetAll<CategoryViewModel>();

            var viewModel = new IndexViewModel()
            {
                LatestProducts = latestProducts,
                BestSellingProducts = bestSellingProducts,
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
