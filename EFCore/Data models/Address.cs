using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Address
{
    public int Id { get; set; }
    public string? City { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public string? Street { get; set; }
    public int? Building { get; set; }
    public int? Flat { get; set; }
    public virtual Person? Person { get; set; }
    public virtual Shop? Shop { get; set; }
}

public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.Street).HasMaxLength(100);
        builder.Property(a => a.Building).HasColumnType("TINYINT");
        builder.Property(a => a.Flat).HasColumnType("TINYINT");
        builder.Property(a => a.Country).HasMaxLength(100);
        builder.Property(a => a.City).HasMaxLength(100);
        builder.Property(a => a.ZipCode).HasMaxLength(50);
    }
}