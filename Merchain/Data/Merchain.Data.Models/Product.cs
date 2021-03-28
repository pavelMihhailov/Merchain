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
            this.ProductsColors = new HashSet<ProductColor>();
            this.CartItems = new HashSet<OrderItem>();
        }

        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Снимки")]
        public string ImagesUrls { get; set; }

        [Display(Name = "Демо Снимка")]
        public string PreviewImage { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [DefaultValue(true)]
        public bool HasSize { get; set; }

        public virtual ICollection<ProductCategory> ProductsCategories { get; set; }

        public virtual ICollection<ProductColor> ProductsColors { get; set; }

        public virtual ICollection<OrderItem> CartItems { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
