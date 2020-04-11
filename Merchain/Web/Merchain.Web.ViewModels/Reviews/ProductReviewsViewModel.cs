namespace Merchain.Web.ViewModels.Reviews
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.Products;

    public class ProductReviewsViewModel
    {
        public ProductDefaultViewModel Product { get; set; }

        public IEnumerable<ReviewDefaultViewModel> Reviews { get; set; }
    }
}
