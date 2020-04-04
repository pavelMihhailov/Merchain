namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Web.ViewModels.Order;
    using Merchain.Web.ViewModels.ShoppingCart;

    public interface IOrderService
    {
        Task<bool> PlaceOrder(IEnumerable<CartItem> cartItems, decimal totalSum, string username);

        Task<bool> PlaceOrder(IEnumerable<CartItem> cartItems, decimal totalSum, string username, OrderAddress address);

        Task<IEnumerable<Order>> AllOrdersOfUser(string userId);

        Task<IEnumerable<MyOrderViewModel>> MyOrders(string username);
    }
}
