namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Products;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ShoppingCartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductsService productsService;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(
            ICartService cartService,
            IProductsService productsService,
            ILogger<ShoppingCartController> logger)
        {
            this.cartService = cartService;
            this.productsService = productsService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var cart = SessionExtension.Get<List<CartItem>>(this.HttpContext.Session, SessionConstants.Cart);

            decimal totalSum = cart != null ?
                cart.Sum(item => item.Product.Price * item.Quantity) : 0;

            var suggestedProducts = this.productsService
                .GetAll<ProductDefaultViewModel>()
                .Take(4);

            var viewModel = new CartViewModel()
            {
                Cart = cart ?? new List<CartItem>(),
                SumTotal = totalSum,
                SuggestedProducts = suggestedProducts,
            };

            this.ViewData[ViewDataConstants.ErrorMessage] = this.TempData[ViewDataConstants.ErrorMessage];

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int id, int quantity, string size, int? colorId)
        {
            try
            {
                if (quantity == 0)
                {
                    return new StatusCodeResult(404);
                }

                await this.cartService.AddToCart(this.HttpContext.Session, id, quantity, size, colorId);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not add product to the cart.\n-{ex.Message}");
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(200);
        }

        [HttpGet]
        public IActionResult RemoveProduct(int id, string size, int? colorId)
        {
            try
            {
                this.cartService.RemoveFromCart(this.HttpContext.Session, id, size, colorId);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not remove product from the cart.\n-{ex.Message}");
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(200);
        }

        [HttpGet]
        public IActionResult DecreaseQuantity(int id, string size, int? colorId)
        {
            try
            {
                this.cartService.DecreaseQuantity(this.HttpContext.Session, id, size, colorId);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not decrease quantity of product in the cart.\n-{ex.Message}");
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(200);
        }

        [HttpGet]
        public int GetCartItemsCount()
        {
            int itemsCount = this.cartService.GetCartItemsCount(this.HttpContext.Session);

            return itemsCount;
        }
    }
}
