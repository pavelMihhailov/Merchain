namespace Merchain.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Common.Models;

    public class Color : BaseDeletableModel<int>
    {
        public Color()
        {
            this.ProductsColors = new HashSet<ProductColor>();
        }

        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Value { get; set; }

        public virtual ICollection<ProductColor> ProductsColors { get; set; }
    }
}
