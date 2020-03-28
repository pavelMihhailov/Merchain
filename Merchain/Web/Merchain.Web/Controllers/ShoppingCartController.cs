namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ShoppingCartController : Controller
    {
        private readonly ICartService cartService;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(ICartService cartService, ILogger<ShoppingCartController> logger)
        {
            this.cartService = cartService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var cart = SessionExtension.Get<List<CartItem>>(this.HttpContext.Session, SessionConstants.Cart);

            decimal totalSum = cart != null ?
                cart.Sum(item => item.Product.Price * item.Quantity) : 0;

            var viewModel = new CartViewModel()
            {
                Cart = cart ?? new List<CartItem>(),
                SumTotal = totalSum,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int id)
        {
            //TODO: Pass quantity as parameter
            try
            {
                await this.cartService.AddToCart(this.HttpContext.Session, id);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not add product to the cart.\n-{ex.Message}");
            }

            //TODO: Remove page redirection, and create pop up for 'Item added'
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveProduct(int id)
        {
            try
            {
                this.cartService.RemoveFromCart(this.HttpContext.Session, id);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not remove product from the cart.\n-{ex.Message}");
            }

            //TODO: Consider redirect or show popuo
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
