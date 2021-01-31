namespace Merchain.Web.ViewModels.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderInfoViewModel
    {
        [Display(Name = "Поръчка ID")]
        public int OrderId { get; set; }

        [Display(Name = "Дата на Поръчване")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Обща Сума")]
        public decimal OrderTotal { get; set; }

        [Display(Name = "Статус")]
        public string OrderStatus { get; set; }

        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public string BadgeStatusCss
        {
            get
            {
                switch (this.OrderStatus)
                {
                    case Common.Order.OrderStatus.Shipped:
                        return "success";
                    case Common.Order.OrderStatus.Accepted:
                        return "primary";
                    case Common.Order.OrderStatus.Pending:
                        return "warning";
                    default:
                        return string.Empty;
                }
            }
        }

        public IEnumerable<OrderedProductsViewModel> ProductsOrdered { get; set; }
    }
}
