namespace Merchain.Web.ViewModels.Order
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class OrderedProductsViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
