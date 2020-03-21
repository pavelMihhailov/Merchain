namespace Merchain.Data.Configurations
{
    using Merchain.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> productCategory)
        {
            productCategory
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            productCategory
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductsCategories)
                .HasForeignKey(x => x.ProductId);

            productCategory
                .HasOne(x => x.Category)
                .WithMany(x => x.ProductsCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
