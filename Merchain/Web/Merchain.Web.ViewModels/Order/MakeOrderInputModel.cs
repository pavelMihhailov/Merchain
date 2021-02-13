namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.ShoppingCart;

    public class MakeOrderInputModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        public decimal Total { get; set; }

        public bool UseRegularAddress { get; set; }

        public int? PromoCodeId { get; set; }
    }
}
