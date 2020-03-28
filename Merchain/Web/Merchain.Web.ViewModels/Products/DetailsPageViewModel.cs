namespace Merchain.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using Merchain.Data.Models;

    public class DetailsPageViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<ProductDefaultViewModel> RelatedProducts { get; set; }
    }
}
