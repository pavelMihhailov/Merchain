namespace Merchain.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Common.Models;

    public class PromoCode : BaseModel<int>
    {
        public string Code { get; set; }

        [Display(Name = "% Discount")]
        public int PercentageDiscount { get; set; }

        [Display(Name = "Valid Until")]
        public DateTime ValidUntil { get; set; }

        [Display(Name = "Is Used")]
        public bool IsUsed { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
