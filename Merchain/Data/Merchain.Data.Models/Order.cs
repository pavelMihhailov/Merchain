namespace Merchain.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Merchain.Data.Common.Constants;
    using Merchain.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrderedItems = new HashSet<OrderItem>();
            this.OrderDate = DateTime.UtcNow;
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        [MaxLength(Requirements.AddressMaxLength)]
        public string Address { get; set; }

        public virtual ICollection<OrderItem> OrderedItems { get; set; }
    }
}
