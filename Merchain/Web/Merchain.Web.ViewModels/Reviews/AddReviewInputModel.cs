namespace Merchain.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using Merchain.Web.ViewModels.Products;

    public class AddReviewInputModel
    {
        public int ProductId { get; set; }

        public ProductDefaultViewModel Product { get; set; }

        [Required(ErrorMessage = "Трябва да изберете оценка.")]
        public int Stars { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
