namespace Merchain.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Common.Order;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Econt.Models.Response;
    using Merchain.Services.Interfaces;
    using Merchain.Services.Messaging;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.Products;
    using Merchain.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Identity;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepo;
        private readonly IOrderItemService orderItemService;
        private readonly IProductsService productsService;
        private readonly IEcontService econtService;
        private readonly SendGridEmailSender emailService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderService(
            IDeletableEntityRepository<Order> orderRepo,
            IOrderItemService orderItemService,
            IProductsService productsService,
            IEcontService econtService,
            SendGridEmailSender emailService,
            UserManager<ApplicationUser> userManager)
        {
            this.orderRepo = orderRepo;
            this.orderItemService = orderItemService;
            this.productsService = productsService;
            this.econtService = econtService;
            this.emailService = emailService;
            this.userManager = userManager;
        }

        public async Task<bool> PlaceOrder(IEnumerable<CartItem> cartItems, string username, OrderAddress address)
        {
            ApplicationUser user = await this.userManager.FindByNameAsync(username);

            decimal totalSum = await this.TotalSum(cartItems);

            Order order = await this.CreateOrder(totalSum, user?.Id, address);

            await this.CreateOrderedItems(order.Id, cartItems, user);

            await this.emailService.SendEmailAsync(
                GlobalConstants.CompanyEmail,
                "Internal",
                GlobalConstants.CompanyEmail,
                "New Order",
                order.ToString());

            return true;
        }

        public Order GetOrderById(int id)
        {
            var order = this.orderRepo.All().FirstOrDefault(x => x.Id == id);

            return order;
        }

        public async Task<IEnumerable<OrderInfoViewModel>> AllOrders()
        {
            var orders = this.orderRepo.All();
            var orderItems = await this.orderItemService.GetAllAsync();

            var viewModel = await this.GetOrdersInfo(orders, orderItems);

            return viewModel.OrderByDescending(x => x.OrderDate);
        }

        public async Task<IEnumerable<Order>> AllOrdersOfUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            var orders = this.orderRepo.All().Where(x => x.UserId == user.Id);

            return orders;
        }

        public async Task<Task> UpdateOrder(Order order)
        {
            if (order == null)
            {
                return null;
            }

            this.orderRepo.Update(order);
            await this.orderRepo.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<OrderInfoViewModel>> OrdersOfUser(string username)
        {
            var orders = await this.AllOrdersOfUser(username);
            var orderItems = await this.orderItemService.GetAllAsync();

            var viewModel = await this.GetOrdersInfo(orders, orderItems);

            return viewModel.OrderByDescending(x => x.OrderDate);
        }

        private async Task<decimal> TotalSum(IEnumerable<CartItem> cartItems)
        {
            decimal totalSum = 0.0m;

            foreach (CartItem item in cartItems)
            {
                Product product = await this.productsService.GetByIdAsync(item.ProductId);

                totalSum += product.Price * item.Quantity;
            }

            return totalSum;
        }

        private async Task<List<OrderInfoViewModel>> GetOrdersInfo(
            IEnumerable<Order> orders, IEnumerable<OrderItem> orderItems)
        {
            var orderInfo = orders
                            .GroupJoin(
                                orderItems,
                                order => order.Id,
                                orderItem => orderItem.OrderId,
                                (order, orderItems) =>
                                    new
                                    {
                                        OrderId = order.Id,
                                        OrderDate = order.OrderDate,
                                        OrderTotal = order.OrderTotal,
                                        UserId = order.UserId,
                                        Address = order.Address,
                                        OrderStatus = order.Status,
                                        ProductsOrdered = orderItems
                                        .Select(p =>
                                            new
                                            {
                                                ProductId = p.ProductId,
                                                Quantity = p.Quantity,
                                            }),
                                    });

            var products = await this.productsService.GetAllAsync<ProductDefaultViewModel>();

            var viewModel = new List<OrderInfoViewModel>();

            foreach (var info in orderInfo)
            {
                var order = new OrderInfoViewModel()
                {
                    OrderId = info.OrderId,
                    OrderDate = info.OrderDate,
                    OrderTotal = info.OrderTotal,
                    OrderStatus = info.OrderStatus,
                    UserId = info.UserId,
                    Address = info.Address,
                };

                var productsOfOrder = new List<OrderedProductsViewModel>();

                foreach (var productItem in info.ProductsOrdered)
                {
                    var matchingProduct = products
                        .FirstOrDefault(x => x.Id == productItem.ProductId);

                    var orderedProduct = new OrderedProductsViewModel()
                    {
                        ProductId = productItem.ProductId,
                        Name = matchingProduct.Name,
                        ImageUrl = matchingProduct.ImagesUrls?.Split(';').ToList().FirstOrDefault(),
                        Price = matchingProduct.Price,
                        Quantity = productItem.Quantity,
                    };

                    productsOfOrder.Add(orderedProduct);
                }

                order.ProductsOrdered = productsOfOrder;
                viewModel.Add(order);
            }

            return viewModel;
        }

        private async Task<Order> CreateOrder(decimal totalSum, string userId, OrderAddress newAddress)
        {
            string address;
            if (!newAddress.ShipToOffice)
            {
                address = this.ConcatenateAddress(
                    newAddress.Address,
                    newAddress.Address2,
                    newAddress.Country,
                    newAddress.Phone);
            }
            else
            {
                Office office = await this.econtService.GetOffice(newAddress.OfficeIdSelected);
                address = $"Град {office.CityName}, Офис - {office.OfficeCode} | {office.Name}";
            }

            var order = new Order()
            {
                UserId = userId,
                Address = address,
                OrderTotal = totalSum,
                Status = OrderStatus.Pending,
            };

            await this.orderRepo.AddAsync(order);
            await this.orderRepo.SaveChangesAsync();

            return order;
        }

        private async Task CreateOrderedItems(int orderId, IEnumerable<CartItem> cartItems, ApplicationUser user)
        {
            var orderedItems = cartItems
                .Select(x => new OrderItem()
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    OrderId = orderId,
                });

            await this.orderItemService.AddOrderItemsAsync(orderedItems);
        }

        private string ConcatenateAddress(string address, string address2, string country, string phone)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Град: {country}");

            this.AddFieldToAddress(sb, address);
            this.AddFieldToAddress(sb, address2);
            this.AddFieldToAddress(sb, $"Телефон: {phone}");

            return sb.ToString();
        }

        private void AddFieldToAddress(StringBuilder sb, string field)
        {
            if (!string.IsNullOrWhiteSpace(field))
            {
                sb.Append($", {field.TrimEnd()}");
            }
        }
    }
}
