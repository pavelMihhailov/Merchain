namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PromoCodesGenerateViewModel
    {
        [Required]
        public int Count { get; set; }

        [Display(Name = "% Discount")]
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
