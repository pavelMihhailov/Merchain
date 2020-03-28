namespace Merchain.Web.ViewModels.Products
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Likes { get; set; }
    }
}
