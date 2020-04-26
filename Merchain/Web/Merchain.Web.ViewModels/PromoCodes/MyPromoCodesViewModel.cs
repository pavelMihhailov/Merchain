namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;

    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class MyPromoCodesViewModel : IMapFrom<PromoCode>
    {
        public string Code { get; set; }

        public int PercentageDiscount { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
