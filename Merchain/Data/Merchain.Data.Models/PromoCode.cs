namespace Merchain.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Common.Models;

    public class PromoCode : BaseModel<int>
    {
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "% Отстъпка")]
        public int PercentageDiscount { get; set; }

        [Display(Name = "Валиден До:")]
        public DateTime ValidUntil { get; set; }

        [Display(Name = "Използван")]
        public bool IsUsed { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
