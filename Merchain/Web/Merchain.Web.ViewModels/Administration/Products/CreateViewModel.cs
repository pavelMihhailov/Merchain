namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Моля въведете валидна цена!")]
        public decimal Price { get; set; }

        [Display(Name = "Категории")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Цветове")]
        public IEnumerable<SelectListItem> Colors { get; set; }
    }
}
