namespace Merchain.Web.ViewModels.ShoppingCart
{
    using Merchain.Data.Models;

    public class CartItem
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
