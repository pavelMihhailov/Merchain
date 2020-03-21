namespace Merchain.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.Shared;

    public class IndexViewModel
    {
        public IEnumerable<ProductHomeViewModel> LatestProducts { get; set; }

        public IEnumerable<ProductHomeViewModel> BestSellingProducts { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
