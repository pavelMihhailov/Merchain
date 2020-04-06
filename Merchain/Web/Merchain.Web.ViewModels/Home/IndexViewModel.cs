namespace Merchain.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.Products;
    using Merchain.Web.ViewModels.Shared;

    public class IndexViewModel
    {
        public IEnumerable<ProductDefaultViewModel> LatestProducts { get; set; }

        public IEnumerable<ProductDefaultViewModel> MostLikedProducts { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
