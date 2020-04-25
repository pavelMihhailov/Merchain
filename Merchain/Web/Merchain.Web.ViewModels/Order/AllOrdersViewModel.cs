namespace Merchain.Web.ViewModels.Order
{
    using System.Collections.Generic;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderInfoViewModel> Orders { get; set; }

        public IList<int> OrdersCountInLast7Days { get; set; }
    }
}
