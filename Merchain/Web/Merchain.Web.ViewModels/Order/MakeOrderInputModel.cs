namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Web.ViewModels.ShoppingCart;

    public class MakeOrderInputModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        public int? PromoCodeId { get; set; }
    }
}
