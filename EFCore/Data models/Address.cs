using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Address
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int PostalCode { get; set; }
    public int Street { get; set; }
    public int Building { get; set; }
    public int? Flat { get; set; }
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
}

public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.Street).HasColumnType("TINYINT");
        builder.Property(a => a.Building).HasColumnType("TINYINT");
        builder.Property(a => a.Flat).HasColumnType("TINYINT");
        builder.Property(a => a.Country).HasColumnType("CHAR(50)");
        builder.Property(a => a.City).HasColumnType("CHAR(50)");
    }
}


