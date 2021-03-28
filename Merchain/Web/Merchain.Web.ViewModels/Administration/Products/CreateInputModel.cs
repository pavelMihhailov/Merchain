namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IFormFile PreviewImage { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IEnumerable<int> Categories { get; set; }

        public IEnumerable<int> Colors { get; set; }
    }
}
