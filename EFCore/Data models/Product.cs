using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public virtual List<Product_Tag> Product_Tags { get; set; } = new();
    public virtual List<Review> Reviews { get; set; } = new();
    public virtual List<Product_Amount> ProductAmounts { get; set; } = new();
    public virtual List<Order> Order { get; set; } = new();
}

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseIdentityColumn();

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(128);

        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasPrecision(10, 2);
    }
}
