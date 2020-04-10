namespace Merchain.Web.ViewModels.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderInfoViewModel
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Date Of Order")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total Cost")]
        public decimal OrderTotal { get; set; }

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Address")]
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
