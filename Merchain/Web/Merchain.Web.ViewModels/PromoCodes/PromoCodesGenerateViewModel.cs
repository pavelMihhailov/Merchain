namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PromoCodesGenerateViewModel
    {
        [Range(1, 1000000)]
        [Required]
        [Display(Name = "Брой")]
        public int Count { get; set; }

        [Display(Name = "% Отстъпка")]
        [Range(1, 99)]
        [Required]
        public int PercentageDiscount { get; set; }

        [Display(Name = "Валиден До:")]
        [Required]
        public DateTime ValidUntil
        {
            get => DateTime.UtcNow.ToLocalTime().AddDays(1);
            set { this.ValidUntil = value; }
        }
    }
}
