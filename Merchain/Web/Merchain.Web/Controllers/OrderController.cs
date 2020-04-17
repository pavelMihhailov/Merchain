namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Common.Order;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly ICartService cartService;
        private readonly ILogger<OrderController> logger;

        public OrderController(
            IProductsService productsService,
            IOrderService orderService,
            IOrderItemService orderItemService,
            ICartService cartService,
            ILogger<OrderController> logger)
        {
            this.productsService = productsService;
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.cartService = cartService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var viewModel = await this.orderService.OrdersOfUser(this.User.Identity.Name);

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cartItems = SessionExtension.Get<List<CartItem>>(this.HttpContext.Session, SessionConstants.Cart);

            var viewModel = new OrderViewModel()
            {
                CartItems = cartItems,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(MakeOrderInputModel inputModel, OrderAddress addressModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "The submitted form was not OK.";

                return this.RedirectToAction("Index", "ShoppingCart");
            }

            try
            {
                inputModel.Total += inputModel.ShippingPaid ? ShippingConstants.NextDay : ShippingConstants.Free;

                var username = this.User.Identity.Name;
                var success = false;

                if (inputModel.UseRegularAddress)
                {
                    success = await this.orderService.PlaceOrder(inputModel.CartItems, inputModel.Total, username);
                }
                else
                {
                    success = await this.orderService.PlaceOrder(inputModel.CartItems, inputModel.Total, username, addressModel);
                }

                if (success)
                {
                    this.cartService.EmptyCart(this.HttpContext.Session);
                    this.TempData[ViewDataConstants.SucccessMessage] = "Thank you for your order.";
                }
                else
                {
                    throw new InvalidOperationException();
                }

                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Could not complete the order!!!\n{ex.Message}");
                this.TempData[ViewDataConstants.ErrorMessage] = "There was a problem submiting your order.";

                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}
