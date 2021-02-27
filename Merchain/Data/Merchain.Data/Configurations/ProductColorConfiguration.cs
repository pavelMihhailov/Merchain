namespace Merchain.Data.Configurations
{
    using Merchain.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> productCategory)
        {
            productCategory
                .HasKey(pc => new { pc.ProductId, pc.ColorId });

            productCategory
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductsColors)
                .HasForeignKey(x => x.ProductId);

            productCategory
                .HasOne(x => x.Color)
                .WithMany(x => x.ProductsColors)
                .HasForeignKey(x => x.ColorId);
        }
    }
}
