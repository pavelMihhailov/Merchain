namespace Merchain.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Order;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrderService orderService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService orderService, IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            var allOrdersViewModel = await this.orderService.AllOrders();

            var pageSize = 8;
            var ordersCount = allOrdersViewModel.Count();

            this.ViewBag.CurrPage = page;
            this.ViewBag.MaxPage = (ordersCount / pageSize) + (ordersCount % pageSize == 0 ? 0 : 1);

            allOrdersViewModel = allOrdersViewModel.Skip(((int)page - 1) * pageSize).Take(pageSize);

            var ordersInLast7Days = new List<int>();

            for (int i = 6; i >= 0; i--)
            {
                ordersInLast7Days.Add(allOrdersViewModel
                    .Where(x =>
                        x.OrderDate.Day == DateTime.UtcNow.Day - i &&
                        x.OrderDate.Month == DateTime.UtcNow.Month &&
                        x.OrderDate.Year == DateTime.UtcNow.Year)
                    .Count());
            }

            var viewModel = new AllOrdersViewModel()
            {
                Orders = allOrdersViewModel,
                OrdersCountInLast7Days = ordersInLast7Days,
            };

            this.HandlePopupMessages();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return this.RedirectToAction("Index");
            }

            var allOrders = await this.orderService.AllOrders();
            var orderViewModel = allOrders.FirstOrDefault(x => x.OrderId == id);

            return this.View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MarkOrderAs(int id, string markedAs)
        {
            var order = this.orderService.GetOrderById(id);

            if (order == null)
            {
                return this.RedirectToAction("Index");
            }

            order.Status = markedAs;
            await this.orderService.UpdateOrder(order);

            this.TempData[ViewDataConstants.SucccessMessage] = "Order status has been updated.";

            return this.RedirectToAction("Details", new { id });
        }
    }
}