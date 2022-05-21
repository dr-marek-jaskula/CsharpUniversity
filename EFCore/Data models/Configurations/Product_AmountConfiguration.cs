using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models.Configurations;

public class ProductAmountEntityTypeConfiguration : IEntityTypeConfiguration<Product_Amount>
{
    public void Configure(EntityTypeBuilder<Product_Amount> builder)
    {
        builder.ToTable("Product_Amount");

        builder.HasKey(pa => new { pa.ProductId, pa.ShopId });

        builder.Property(pa => pa.Amount)
            .IsRequired(true)
            .HasColumnType("INT");
    }
}