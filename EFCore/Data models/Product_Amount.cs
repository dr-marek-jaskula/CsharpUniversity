using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Product_Amount
{
    public int Amount { get; set; }
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Shop? Shop { get; set; }
    public int? ShopId { get; set; }
}

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