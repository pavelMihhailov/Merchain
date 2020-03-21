namespace Merchain.Web.ViewModels.Home
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ProductHomeViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}