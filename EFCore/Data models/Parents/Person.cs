using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

//Table-per-type approach
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public int? AddressId { get; set; }
}

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("SMALLINT").UseIdentityColumn();

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
            .HasColumnType("VARCHAR(7)")
            .HasConversion(g => g.ToString(),
            s => (Gender)Enum.Parse(typeof(Gender), s))
            .HasComment("Male, Female or Unknown");

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Person)
            .HasForeignKey<Person>(c => c.AddressId);
    }
}