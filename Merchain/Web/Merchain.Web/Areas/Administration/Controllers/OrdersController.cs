namespace Merchain.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
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

            return this.View(allOrdersViewModel);
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
    }
}