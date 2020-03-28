namespace Merchain.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            return this.View();
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
    }
}