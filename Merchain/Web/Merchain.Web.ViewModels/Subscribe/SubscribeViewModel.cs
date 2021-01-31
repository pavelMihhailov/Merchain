namespace Merchain.Web.ViewModels.Subscribe
{
    using System.ComponentModel.DataAnnotations;

    public class SubscribeViewModel
    {
        [Required]
        [Display(Name = "Имейл")]
        public string Email { get; set; }
    }
}
