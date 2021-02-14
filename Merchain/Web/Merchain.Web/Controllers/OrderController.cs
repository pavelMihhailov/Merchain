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
    using Merchain.Services.Econt.Models.Response;
    using Merchain.Services.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Econt;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IPromoCodesService promoCodesService;
        private readonly IEcontService econtService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<OrderController> logger;

        public OrderController(
            IOrderService orderService,
            ICartService cartService,
            IPromoCodesService promoCodesService,
            IEcontService econtService,
            UserManager<ApplicationUser> userManager,
            ILogger<OrderController> logger)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.promoCodesService = promoCodesService;
            this.econtService = econtService;
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

            ApplicationUser user = null;
            if (this.User.Identity.IsAuthenticated)
            {
                user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            }

            var viewModel = new OrderViewModel()
            {
                CartItems = cartItems,
                Total = cartItems.Sum(x => x.Product.Price * x.Quantity),
                UserHasAddressByDefault = this.UserHasDefaultAddress(user),
            };

            IQueryable<Office> econtOffices = await this.econtService.GetOffices();
            viewModel.Offices = econtOffices.To<OfficeViewModel>();

            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                this.ApplyPromoCode(promoCode, viewModel, user.Id);
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
                var username = this.User.Identity.Name;
                bool success = false;

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
                    this.TempData[ViewDataConstants.SucccessMessage] = "Поръчката Ви беше приета успешно.";
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

        private void ApplyPromoCode(string promoCode, OrderViewModel viewModel, string userId)
        {
            var promoCodeFromDb = this.promoCodesService.GetByCodeAsync(userId, promoCode);
            if (promoCodeFromDb != null)
            {
                viewModel.Total -= Math.Round(viewModel.Total * promoCodeFromDb.PercentageDiscount / 100, 2);
                viewModel.AppliedPromoCode = promoCodeFromDb;
            }
        }

        private bool UserHasDefaultAddress(ApplicationUser user)
        {
            if (user != null &&
                !string.IsNullOrWhiteSpace(user.Address) &&
                !string.IsNullOrWhiteSpace(user.PhoneNumber) &&
                !string.IsNullOrWhiteSpace(user.Country))
            {
                return true;
            }

            return false;
        }

    }
}
