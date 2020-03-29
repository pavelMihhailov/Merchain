namespace Merchain.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using Merchain.Web.ViewModels.Categories;

    public class ProductsIndexViewModel
    {
        public IEnumerable<CategoryDefaultViewModel> Categories { get; set; }

        public IEnumerable<ProductDefaultViewModel> Products { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
    }
}
