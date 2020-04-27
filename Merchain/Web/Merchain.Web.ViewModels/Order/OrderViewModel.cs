namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;

    using Merchain.Data.Models;
    using Merchain.Web.ViewModels.ShoppingCart;

    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }

        public decimal Total { get; set; }

        public PromoCode AppliedPromoCode { get; set; }
    }
}
