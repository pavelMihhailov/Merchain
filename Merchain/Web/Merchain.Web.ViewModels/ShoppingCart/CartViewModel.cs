namespace Merchain.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Merchain.Data.Models;

    public class CartViewModel
    {
        public IEnumerable<CartItem> Cart { get; set; }

        public decimal SumTotal { get; set; }
    }
}
