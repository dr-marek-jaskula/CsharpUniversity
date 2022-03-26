using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Customer : Person
{
    public int Id { get; set; }
    public Rank Rank { get; set; }
    public List<Order> Orders { get; set; } = new();
}

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).UseIdentityColumn();

        builder.Property(c => c.FirstName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.ContactNumber)
            .IsRequired(true)
            .HasMaxLength(40);

        builder.Property(c => c.DateOfBirth)
            .HasColumnType("DATE")
            .HasDefaultValue(null);

        builder.Property(c => c.Gender)
            .IsRequired(true)
            .HasColumnType("CHAR(7)")
            .HasConversion(g => g.ToString(),
            s => (Gender)Enum.Parse(typeof(Gender), s))
            .HasComment("Male, Female or Unknown");

        builder.Property(c => c.Rank)
            .HasDefaultValue(Rank.Standard)
            .HasColumnType("CHAR(8)")
            .HasConversion(r => r.ToString(),
            s => (Rank)Enum.Parse(typeof(Rank), s));

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Customer)
            .HasForeignKey<Customer>(c => c.AddressId);
    }
}

