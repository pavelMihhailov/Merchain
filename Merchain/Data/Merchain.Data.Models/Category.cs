namespace Merchain.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.ProductsCategories = new HashSet<ProductCategory>();
        }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<ProductCategory> ProductsCategories { get; set; }
    }
}
