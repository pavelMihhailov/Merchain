namespace Merchain.Data.Configurations
{
    using Merchain.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
    {
        public void Configure(EntityTypeBuilder<UserOrder> userOrder)
        {
            userOrder
                .HasKey(x => new { x.UserId, x.OrderId });
        }
    }
}
