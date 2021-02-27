namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public int[] SelectedCategories { get; set; }

        public int[] SelectedColors { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        public bool HasSize { get; set; }

        public IEnumerable<string> Images
        {
            get
            {
                return this.Product.ImagesUrls.Split(';').ToList();
            }
        }
    }
}
