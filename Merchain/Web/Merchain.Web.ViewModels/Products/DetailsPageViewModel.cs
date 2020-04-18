namespace Merchain.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Data.Models;

    public class DetailsPageViewModel
    {
        public Product Product { get; set; }

        public int AvgStars { get; set; }

        public int ReviewsCount { get; set; }

        public IEnumerable<ProductDefaultViewModel> RelatedProducts { get; set; }

        public IEnumerable<string> ImageUrls
        {
            get
            {
                return this.Product.ImagesUrls.Split(';').ToList();
            }
        }
    }
}
