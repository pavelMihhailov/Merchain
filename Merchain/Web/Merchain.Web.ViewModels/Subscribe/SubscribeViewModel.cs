namespace Merchain.Web.ViewModels.Subscribe
{
    using System.ComponentModel.DataAnnotations;

    public class SubscribeViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
