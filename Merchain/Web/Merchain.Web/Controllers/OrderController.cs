namespace Merchain.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Extensions;
    using Merchain.Common.Order;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly ICartService cartService;
        private readonly IPromoCodesService promoCodesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<OrderController> logger;

        public OrderController(
            IProductsService productsService,
            IOrderService orderService,
            IOrderItemService orderItemService,
            ICartService cartService,
            IPromoCodesService promoCodesService,
            UserManager<ApplicationUser> userManager,
            ILogger<OrderController> logger)
        {
            this.productsService = productsService;
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.cartService = cartService;
            this.promoCodesService = promoCodesService;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var viewModel = await this.orderService.OrdersOfUser(this.User.Identity.Name);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string promoCode)
        {
            var cartItems = SessionExtension.Get<List<CartItem>>(this.HttpContext.Session, SessionConstants.Cart);

            if (cartItems == null)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "You do not have any products added in cart.";

                return this.RedirectToAction("Index", "ShoppingCart");
            }

            var viewModel = new OrderViewModel()
            {
                CartItems = cartItems,
                Total = cartItems.Sum(x => x.Product.Price * x.Quantity),
            };

            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

                var promoCodeFromDb = this.promoCodesService.GetByCodeAsync(user.Id, promoCode);
                if (promoCodeFromDb != null)
                {
                    viewModel.Total -= Math.Round(viewModel.Total * promoCodeFromDb.PercentageDiscount / 100, 2);
                    viewModel.AppliedPromoCode = promoCodeFromDb;
                }
            }

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
                    if (inputModel.PromoCodeId != null)
                    {
                        await this.promoCodesService.MarkAsUsed((int)inputModel.PromoCodeId);
                    }

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
