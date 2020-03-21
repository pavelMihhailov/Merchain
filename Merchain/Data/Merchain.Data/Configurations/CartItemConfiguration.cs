namespace Merchain.Data.Configurations
{
    using Merchain.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> cartItem)
        {
            cartItem
                .HasOne(x => x.Product)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.ProductId);

            cartItem
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderedItems)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
