namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PromoCodesGenerateViewModel
    {
        [Range(1, 1000000)]
        [Required]
        public int Count { get; set; }

        [Display(Name = "% Discount")]
        [Range(1, 99)]
        [Required]
        public int PercentageDiscount { get; set; }

        [Required]
        public DateTime ValidUntil
        {
            get => DateTime.UtcNow.ToLocalTime().AddDays(1);
            set { this.ValidUntil = value; }
        }
    }
}
