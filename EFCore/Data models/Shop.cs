using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Shop
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public virtual Address? Address { get; set; }
    public int? AddressId { get; set; }
    public virtual List<Employee> Employees { get; set; } = new();
    public virtual List<Order> Orders { get; set; } = new();
    public virtual List<Product_Amount> ProductAmounts { get; set; } = new();
}

public class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shop");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasMaxLength(128);

        builder.Property(s => s.Description)
            .HasMaxLength(1000);

        builder.HasOne(s => s.Address)
            .WithOne(a => a.Shop)
            .HasForeignKey<Shop>(s => s.AddressId);

        builder.HasMany(s => s.Employees)
            .WithOne(e => e.Shop)
            .HasForeignKey(e => e.ShopId);

        builder.HasMany(s => s.Orders)
            .WithOne(o => o.Shop)
            .HasForeignKey(o => o.ShopId);
    }
}