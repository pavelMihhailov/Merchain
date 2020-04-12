namespace Merchain.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly IProductsService productsService;

        public SearchController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index(string query)
        {
            var products = this.productsService.GetAll<ProductDefaultViewModel>();

            var matchedProducts = new List<ProductDefaultViewModel>();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();

                matchedProducts = products
                    .Where(s =>
                    s.Name.ToLower().Contains(query) ||
                    s.Id.ToString() == query).ToList();
            }

            return this.View(matchedProducts);
        }
    }
}
