namespace Merchain.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Merchain.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.ProductsCategories = new HashSet<ProductCategory>();
            this.CartItems = new HashSet<OrderItem>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Images")]
        public string ImagesUrls { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        [Column(Order = 10)]
        [DefaultValue(0)]
        public int Likes { get; set; }

        public virtual ICollection<ProductCategory> ProductsCategories { get; set; }

        public virtual ICollection<OrderItem> CartItems { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
