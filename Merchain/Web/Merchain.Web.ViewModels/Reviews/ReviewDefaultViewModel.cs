namespace Merchain.Web.ViewModels.Reviews
{
    using System;

    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class ReviewDefaultViewModel : IMapFrom<Review>
    {
        public int Stars { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
