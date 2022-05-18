using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCore;

//We need to create a custom DbContext (here named "MyDbContext") that inherits from the DbContext class
//Using this class we will connect manipulate the database entities by manipulating class properties (DbSet)

public class MyDbContext : DbContext
{
    //1. In this class we need to write the following constructor
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    //2. DbSets should be determine here

    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
    public DbSet<Issue> Issues => Set<Issue>();
    public DbSet<Data_models.Task> Tasks => Set<Data_models.Task>();
    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Person> People => Set<Person>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<Salary_Transfer> SalaryTransfers => Set<Salary_Transfer>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();

    public DbSet<Product_Tag> ProductTags => Set<Product_Tag>();
    public DbSet<Product_Amount> ProductAmounts => Set<Product_Amount>();

    //3. The OnModelCreating method should be overridden
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //4. My preferred way is to get configuration from the Assembly (from each file separately). Nevertheless, it can be done all here
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //5. The exception (not required, it still can be done in certain model files) is when we apply brand new approach for many-to-many relationship:
        //Use models even if the c# models relations are direct

        //Product_Tag
        builder.Entity<Product>(eb =>
        {
            eb.HasMany(p => p.Tags).WithMany(t => t.Products)
                .UsingEntity<Product_Tag>(
                    //two lambdas for many-to-many relation. (If there is no reference in the class, then the WithMany() should not have any parameters)
                    p => p.HasOne(pt => pt.Tag).WithMany().HasForeignKey(pt => pt.TagId),
                    p => p.HasOne(pt => pt.Product).WithMany().HasForeignKey(pt => pt.ProductId),
                    //configuring Protuct_Tag class
                    pt =>
                    {
                        pt.HasKey(x => new { x.TagId, x.ProductId });
                    });
        });

        //Product_Amount (with additional data: "Amount")
        builder.Entity<Product>(eb =>
        {
            eb.HasMany(p => p.Shops).WithMany(s => s.Products)
                .UsingEntity<Product_Amount>(
                    p => p.HasOne(pa => pa.Shop).WithMany().HasForeignKey(pa => pa.ShopId),
                    p => p.HasOne(pa => pa.Product).WithMany().HasForeignKey(pa => pa.ProductId),
                    pt =>
                    {
                        pt.HasKey(x => new { x.ShopId, x.ProductId });
                    });
        });
    }
}

//Sometimes the factory is placed in the same file as DbContext
//For educational purpose we will store them separately