namespace Merchain.Web.ViewModels.Subscribe
{
    using System.ComponentModel.DataAnnotations;

    using Merchain.Common.CustomAttributes;

    public class SubscribeViewModel
    {
        [Required(ErrorMessage = "Имейл полето е задължително.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [RequiredConsentAttribute(ErrorMessage = "Трябва да се съгласите с условията.")]
        [Display(Name = "Съгласен съм с условията ...")]
        public bool MarketingConsent { get; set; }
    }
}
