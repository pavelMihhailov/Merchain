namespace Merchain.Web.ViewModels.Colors
{
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ColorViewModel : IMapFrom<Color>
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Код")]
        public string Value { get; set; }
    }
}
