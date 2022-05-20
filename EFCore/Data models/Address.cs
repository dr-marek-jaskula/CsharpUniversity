using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.Data_models.Owned;

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

    //Owned reference
    public Coordinate? Coordinate { get; set; }
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

        //Owned configuration:
        builder.OwnsOne(a => a.Coordinate, ab =>
        {
            ab.Property(c => c.Latitude)
            .HasPrecision(18, 7)
            .HasColumnName("Latitude"); //In order to avoid "Coordinate_Latitude"

            ab.Property(c => c.Longitude)
            .HasPrecision(18, 7)
            .HasColumnName("Longitude"); //In order to avoid "Coordinate_Longitude"
        });
    }
}