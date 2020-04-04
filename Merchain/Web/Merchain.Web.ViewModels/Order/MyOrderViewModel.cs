namespace Merchain.Web.ViewModels.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MyOrderViewModel
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Date Of Order")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total Cost")]
        public decimal OrderTotal { get; set; }

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        public IEnumerable<OrderedProductsViewModel> ProductsOrdered { get; set; }
    }
}
