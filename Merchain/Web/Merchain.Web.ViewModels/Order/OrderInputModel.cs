namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.ShoppingCart;

    public class OrderInputModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
