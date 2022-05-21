using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models.Configurations;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.Property(c => c.Rank)
            .HasDefaultValue(Rank.Standard)
            .HasColumnType("VARCHAR(8)")
            .HasConversion(r => r.ToString(),
            s => (Rank)Enum.Parse(typeof(Rank), s));
    }
}