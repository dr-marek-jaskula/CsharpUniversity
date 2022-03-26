using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Employee : Person
{
    public int Id { get; set; }
    public DateTime HireDate { get; set; }
    public Salary Salary { get; set; } = new();
    public int SalaryId { get; set; }
    public Shop Shop { get; set; } = new();
    public int ShopId { get; set; }
    public List<Customer> Customers { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
}

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee");

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

        builder.Property(c => c.HireDate)
            .HasColumnType("DATE")
            .HasDefaultValue(null);

        builder.HasOne(e => e.Salary)
            .WithOne(s => s.Employee)
            .HasForeignKey<Employee>(e => e.SalaryId);

        builder.HasOne(e => e.Address)
            .WithOne(a => a.Employee)
            .HasForeignKey<Employee>(e => e.AddressId);
    }
}

