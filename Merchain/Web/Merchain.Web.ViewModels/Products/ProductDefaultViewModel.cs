namespace Merchain.Web.ViewModels.Products
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ProductDefaultViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}