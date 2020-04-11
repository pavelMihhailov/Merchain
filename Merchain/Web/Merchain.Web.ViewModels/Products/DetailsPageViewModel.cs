﻿namespace Merchain.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using Merchain.Data.Models;

    public class DetailsPageViewModel
    {
        public Product Product { get; set; }

        public int AvgStars { get; set; }

        public int ReviewsCount { get; set; }

        public IEnumerable<ProductDefaultViewModel> RelatedProducts { get; set; }
    }
}
