namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Please enter a valid price!")]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
