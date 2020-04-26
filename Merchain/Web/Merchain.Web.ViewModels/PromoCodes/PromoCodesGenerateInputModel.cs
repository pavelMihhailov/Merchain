namespace Merchain.Web.ViewModels.PromoCodes
{
    using System;

    public class PromoCodesGenerateInputModel
    {
        public int Count { get; set; }

        public int PercentageDiscount { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
