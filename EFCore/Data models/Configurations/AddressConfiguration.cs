using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models.Configurations;

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