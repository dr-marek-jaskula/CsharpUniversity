using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class ProductAmount
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public Product Product { get; set; } = new();
    public int ProductId { get; set; } = new();
    public Shop Shop { get; set; } = new();
    public int ShopId { get; set; } = new();
}

public class ProductAmountEntityTypeConfiguration : IEntityTypeConfiguration<ProductAmount>
{
    public void Configure(EntityTypeBuilder<ProductAmount> builder)
    {
        builder.ToTable("product_amount");

        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.Id).UseIdentityColumn();

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

