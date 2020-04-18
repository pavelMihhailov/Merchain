namespace Merchain.Web.ViewModels.Products
{
    using System.Linq;

    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ProductDefaultViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagesUrls { get; set; }

        public decimal Price { get; set; }

        public string DefaultImage
        {
            get
            {
                return this.ImagesUrls?.Split(';').ToList().FirstOrDefault();
            }
        }
    }
}
