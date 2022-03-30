using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EFCore;

//We need to create a custom DbContext (here named "MyDbContext") that inherits from the DbContext class
//Using this class we will connect manipulate the database entities by manipulating class properties (DbSet)

public class MyDbContext : DbContext
{
    //1. In this class we need to write the following constructor
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    //2. DbSets should be determine here
    
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Product_Tag> Products_Tag => Set<Product_Tag>();
    public DbSet<Product_Amount> ProductAmounts => Set<Product_Amount>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<Salary_Transfer> SalaryTransfers => Set<Salary_Transfer>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Tag> Tags => Set<Tag>();

    //3. The OnModelCreating method should be overridden
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //4 My preferred way is to get configuration from the Assembly (from each file separately). Nevertheless, it can be done all here
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

//Sometimes the factory is placed in the same file as DbContext
//For educational purpose we will store them separately