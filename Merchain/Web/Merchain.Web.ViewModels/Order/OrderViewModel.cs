namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Web.ViewModels.ShoppingCart;

    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }

        public decimal Total => this.CartItems.Sum(x => x.Product.Price * x.Quantity);
    }
}
