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
        builder.ToTable("product_amount");

        builder.HasKey(pa => new { pa.ProductId, pa.ShopId });

        builder.Property(pa => pa.Amount)
            .IsRequired(true)
            .HasColumnType("INT");

        builder.HasOne(pa => pa.Product)
           .WithMany(p => p.ProductAmounts)
           .HasForeignKey(pt => pt.ProductId);

        builder.HasOne(pa => pa.Shop)
            .WithMany(s => s.ProductAmounts)
            .HasForeignKey(pa => pa.ShopId);
    }
}

