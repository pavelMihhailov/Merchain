namespace Merchain.Data.Configurations
{
    using Merchain.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> orderItem)
        {
            orderItem
                .HasOne(x => x.Product)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.ProductId);

            orderItem
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderedItems)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
