namespace Merchain.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<int> Categories { get; set; }
    }
}
