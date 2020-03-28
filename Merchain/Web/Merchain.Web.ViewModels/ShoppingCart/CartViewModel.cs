namespace Merchain.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;

    using Merchain.Data.Models;
    using Merchain.Web.ViewModels.Products;

    public class CartViewModel
    {
        public IEnumerable<CartItem> Cart { get; set; }

        public decimal SumTotal { get; set; }

        public IEnumerable<ProductDefaultViewModel> SuggestedProducts { get; set; }
    }
}
