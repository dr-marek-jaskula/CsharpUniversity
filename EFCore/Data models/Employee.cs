using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Employee : Person
{
    //Properties that defines the database relations should be mark as virtual
    public DateTime HireDate { get; set; }

    //One to one relationship with Salary table (Salary, SalaryId)
    public virtual Salary? Salary { get; set; }

    public int? SalaryId { get; set; }

    //One to many relationship with Shop table (Shop, ShopId)
    public virtual Shop? Shop { get; set; }

    public int? ShopId { get; set; }

    //One to one relationship with User table (User, UserId)
    public virtual User? User { get; set; }

    //Many to many relationship with customers (rest is in Customer class)
    public virtual List<Customer> Customers { get; set; } = new();

    //Many to many relationship with Reviews (rest is in Reviews class)
    public virtual List<Review> Reviews { get; set; } = new();

    //One to many relationship with same table (ManagerId, Manager, Subordinates)
    public virtual int? ManagerId { get; set; }

    public virtual Employee? Manager { get; set; }
    public virtual List<Employee>? Subordinates { get; set; } = new();

    //WorkItems relations
    public virtual EFCore.Data_models.Task? CurrentTask { get; set; }

    public virtual Project? Project { get; set; }
}

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.Property(e => e.HireDate)
            .HasColumnType("DATE")
            .HasDefaultValue(null);

        //Defining the relations: (rest are in Customer and Review classes)

        builder.HasOne(e => e.Salary)
            .WithOne(s => s.Employee)
            .HasForeignKey<Employee>(e => e.SalaryId);

        builder.HasOne(e => e.Manager)
            .WithMany(x => x.Subordinates)
            .HasForeignKey(e => e.ManagerId)
            .IsRequired(false);
    }
}