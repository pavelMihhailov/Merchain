namespace Merchain.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Range(0, 500)]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
