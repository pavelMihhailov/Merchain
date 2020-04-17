namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;

    using Merchain.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public int[] SelectedCategories { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
