namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class MyPromoCodesViewModel : IMapFrom<PromoCode>
    {
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "Процент Отстъпка")]
        public int PercentageDiscount { get; set; }

        [Display(Name = "Валиден До")]
        public DateTime ValidUntil { get; set; }
    }
}
