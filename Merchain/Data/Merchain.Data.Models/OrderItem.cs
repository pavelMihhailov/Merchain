namespace Merchain.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Merchain.Data.Common.Models;

    public class OrderItem : BaseModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Range(0, 500)]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
