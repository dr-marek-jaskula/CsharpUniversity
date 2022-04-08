using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Employee : Person
{
    //Properties that defined the database relations should be mark as virtual
    public int Id { get; set; }
    public DateTime HireDate { get; set; }

    //One to one relationship with Salary table (Salary, SalaryId)
    public Salary? Salary { get; set; }
    public int? SalaryId { get; set; }

    //One to one relationship with Shop table (Shop, ShopId)
    public Shop? Shop { get; set; }
    public int? ShopId { get; set; }

    //Many to many relationship with customers (rest is in Customer class)
    public virtual List<Customer> Customers { get; set; } = new();

    //Many to many relationship with Reviews (rest is in Reviews class)
    public virtual List<Review> Reviews { get; set; } = new();
    
    //One to many relationship with same table (ManagerId, Manager, Subordinates)
    public virtual int? ManagerId { get; set; }
    public virtual Employee? Manager { get; set; }
    public virtual List<Employee>? Subordinates { get; set; } = new();
}

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.FirstName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(e => e.Email)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(e => e.ContactNumber)
            .IsRequired(true)
            .HasMaxLength(40);

        builder.Property(e => e.DateOfBirth)
            .HasColumnType("DATE")
            .HasDefaultValue(null);

        builder.Property(e => e.Gender)
            .IsRequired(true)
            .HasColumnType("VARCHAR(7)")
            .HasConversion(g => g.ToString(),
            s => (Gender)Enum.Parse(typeof(Gender), s))
            .HasComment("Male, Female or Unknown");

        builder.Property(e => e.HireDate)
            .HasColumnType("DATE")
            .HasDefaultValue(null);

        //Defining the relations: (rest are in Customer and Review classes)

        builder.HasOne(e => e.Salary)
            .WithOne(s => s.Employee)
            .HasForeignKey<Employee>(e => e.SalaryId);

        builder.HasOne(e => e.Address)
            .WithOne(a => a.Employee)
            .HasForeignKey<Employee>(e => e.AddressId);

        builder.HasOne(e => e.Manager)
            .WithMany(x => x.Subordinates)
            .HasForeignKey(e => e.ManagerId)
            .IsRequired(false);
    }
}

