namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.ShoppingCart;

    public interface IOrderService
    {
        Task<bool> PlaceOrder(IEnumerable<CartItem> cartItems, string username, OrderAddress address = null);

        Order GetOrderById(int id);

        Task<IEnumerable<OrderInfoViewModel>> AllOrders();

        Task<IEnumerable<Order>> AllOrdersOfUser(string userId);

        Task<Task> UpdateOrder(Order order);

        Task<IEnumerable<OrderInfoViewModel>> OrdersOfUser(string username);
    }
}
