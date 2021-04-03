namespace Merchain.Web.ViewModels.Email
{
    using System.ComponentModel.DataAnnotations;

    public class EmailInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Subject { get; set; }

        [StringLength(10000000, MinimumLength = 2)]
        public string Content { get; set; }
    }
}
