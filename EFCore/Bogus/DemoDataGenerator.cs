using Bogus;
using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore.BogusDemo;

public class DemoDataGenerator
{
    private readonly MyDbContext _context;

    private static Bogus.DataSets.Name.Gender ReturnGenderType(Gender gender)
    {
        if (gender is Gender.Male)
            return Bogus.DataSets.Name.Gender.Male;
        
        return Bogus.DataSets.Name.Gender.Female;
    }

    public DemoDataGenerator(MyDbContext context)
    {
        _context = context;
    }

    public void ClearDatabase()
    {
        //-- Disable all constraints
        //-- Delete data in all tables and use SET QUOTED_IDENTIFIES ON -> (QUOTED_IDENTIFIER controls the behavior of SQL Server handling double-quotes)
        //-- Enable all constraints
        //-- Reseed identity columns if the table has identity
        _context.Database.ExecuteSqlRaw(@"
            EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT all';
            EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?'
            EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';
            EXEC sp_MSForEachTable 'IF (OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1) DBCC CHECKIDENT (''?'', RESEED, 0)';
            ");

        _context.ChangeTracker.Clear();
    }

    public void SeedDatabase()
    {
        //Faker class is from the Bogus NuGet package

        //Roles
        if (!_context.Roles.Any())
        {
            var roles = new Role[] {
                new Role() { Name = "Customer" },
                new Role() { Name = "Employee" },
                new Role() { Name = "Manager" },
                new Role() { Name = "Administrator" }
            };
            _context.Roles.AddRange(roles);
            _context.SaveChanges();
        }

        //In order to generate random data base on a certain seed (to generate same random data every time) we need to use class "Randomizer"
        //All we need to do is to:
        //Randomizer.Seed = new Random(991);
        //Where the hard-coded number 991 is our seed.

        //In order to generate fake data connected to other languages like German or  Polish we can:
        //Use the "Faker" class constructor overload with one string input. This input determines which language will be used for fake data
        //By default it is english and the string is "en"
        //string locale = "en";
        //For Polish it is "pl" and for German it is "de".
        //For more locals visit "https://github.com/bchavez/Bogus"

        //Addresses
        var addressFaker = new Faker<EFCore.Data_models.Address>()
            //.StrictMode(true) //Strict mode throws exception when there exits a property we do not have RuleFor (so we do not seed). Commented coz of "Id"
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Building, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 30) : null) //Bool(0.9f) means give me 90% time true and 10% false
            .RuleFor(a => a.Flat, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 90) : null);

        var addresses = addressFaker.Generate(5000); //this generates 5000 random addresses.

        _context.Addresses.AddRange(addresses);
        _context.SaveChanges();

        //SalaryTransfers
        var salaryTransferFaker = new Faker<Salary_Transfer>()
            .RuleFor(p => p.IsIncentiveBonus, f => f.Random.Bool())
            .RuleFor(p => p.IsTaskBonus, f => f.Random.Bool())
            .RuleFor(p => p.IsDiscretionaryBonus, f => f.Random.Bool())
            .RuleFor(p => p.Date, f => f.Date.Between(DateTime.Now - new TimeSpan(180, 3, 0, 0, 0), DateTime.Now));

        var salaryTransfers = salaryTransferFaker.Generate(2050);

        _context.SalaryTransfers.AddRange(salaryTransfers);
        _context.SaveChanges();

        //Salary
        var salaryFaker = new Faker<Salary>()
            .RuleFor(p => p.BaseSalary, f => f.Random.Int(2000, 5000))
            .RuleFor(p => p.DiscretionaryBonus, f => f.Random.Int(100, 500))
            .RuleFor(p => p.IncentiveBonus, f => f.Random.Int(100, 500))
            .RuleFor(p => p.TaskBonus, f => f.Random.Int(100, 500))
            .RuleFor(p => p.SalaryTransfer, f => { f.IndexVariable++; return salaryTransfers.Skip(20 * (f.IndexVariable - 1)).Take(20).ToList(); });

        var salaries = salaryFaker.Generate(100);

        _context.Salaries.AddRange(salaries);
        _context.SaveChanges();

        //Reviews
        var reviewTitles = new[] { "So bad...", "Great!", "Not bad", "Be careful", "I like", "Good", "Ehhh...", "???", "!!!" };

        var reviewsFaker = new Faker<Review>()
            .RuleFor(p => p.Stars, f => f.Random.Int(0, 5))
            .RuleFor(p => p.UserName, f => f.Name.Random.Word())
            .RuleFor(p => p.Description, f => f.Random.Words(4))
            .RuleFor(p => p.Title, f => f.PickRandom(reviewTitles));

        var reviews = reviewsFaker.Generate(1000);

        _context.Reviews.AddRange(reviews);
        _context.SaveChanges();

        //Employees
        var employeeFaker = new Faker<Employee>()
            .RuleFor(e => e.Gender, f => f.PickRandom<Gender>())
            .RuleFor(e => e.FirstName, (f, e) => f.Name.FirstName(ReturnGenderType(e.Gender)))
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(23, DateTime.Now)))
            .RuleFor(e => e.HireDate, f => f.Date.Past(3, DateTime.Now))
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(e => e.ContactNumber, f => f.Phone.PhoneNumber("### ### ###"))
            .RuleFor(e => e.Address, f => f.PickRandom(addresses));

        var employees = employeeFaker.Generate(100);

        for (int i = 0; i < employees.Count; i++)
        {
            if (i % 20 is 0)
                continue;

            employees[i].Manager = employees[20 * (i / 20)];
        }

        _context.Employees.AddRange(employees);
        _context.SaveChanges();

        foreach (Employee employee in employees)
        {
            employee.Salary = salaries[employees.IndexOf(employee)];
            employee.Reviews.AddRange(reviews.Skip(5 * (employees.IndexOf(employee) - 1)).Take(5));
        }

        _context.SaveChanges();

        //WorkItems:
        //Tasks
        var tasksFaker = new Faker<WorkTask>()
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(t => t.Status, f => f.PickRandom(Status.Received, Status.InProgress))
            .RuleFor(t => t.Description, f => f.Random.Words(20))
            .RuleFor(t => t.Title, f => f.Random.Word())
            .RuleFor(t => t.Priority, f => f.Random.Number(1, 10))
            .RuleFor(t => t.StartDate, f => f.Date.Past(1, DateTime.Now))
            .RuleFor(t => t.EndDate, f => f.Date.Future(1, DateTime.Now));

        var tasks = tasksFaker.Generate(20);
        tasks.ForEach(t =>
        {
            t.Employee = employees.First(e => e.Manager is not null && e.CurrentTask is null);
            t.Employee.CurrentTask = t;
        });

        //Issues
        var issuesFaker = new Faker<Issue>()
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(t => t.Status, f => f.PickRandom(Status.Received, Status.InProgress))
            .RuleFor(t => t.Description, f => f.Random.Words(20))
            .RuleFor(t => t.Title, f => f.Random.Word())
            .RuleFor(t => t.Priority, f => f.Random.Number(1, 10))
            .RuleFor(t => t.Cost, f => f.Random.Decimal(1, 2000));

        var issues = issuesFaker.Generate(8);

        //Projects
        var projectsFaker = new Faker<Project>()
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(t => t.Status, f => f.PickRandom(Status.Received, Status.InProgress))
            .RuleFor(t => t.Description, f => f.Random.Words(20))
            .RuleFor(t => t.Title, f => f.Random.Word())
            .RuleFor(t => t.Priority, f => f.Random.Number(1, 10));

        var projects = projectsFaker.Generate(2);
        projects.ForEach(p =>
        {
            p.ProjectLeader = employees.First(e => e.Manager is null && e.Project is null);
            p.ProjectLeader.Project = p;
        });

        _context.Issues.AddRange(issues);
        _context.Tasks.AddRange(tasks);
        _context.Projects.AddRange(projects);

        _context.SaveChanges();

        //Customers
        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.FirstName, (f, e) => f.Name.FirstName(ReturnGenderType(e.Gender)))
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(23, DateTime.Now)))
            .RuleFor(c => c.Rank, f => f.PickRandom<Rank>())
            .RuleFor(c => c.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(c => c.ContactNumber, f => f.Phone.PhoneNumber("### ### ###"))
            .RuleFor(c => c.Address, f => f.PickRandom(addresses));

        var customers = customerFaker.Generate(400);

        _context.Customers.AddRange(customers);
        _context.SaveChanges();

        foreach (Employee employee in employees)
            employee.Customers.AddRange(customers.Skip(3 * (employees.IndexOf(employee) - 1)).Take(3));

        _context.SaveChanges();

        //Tags
        var tagFaker = new Faker<Tag>()
            .RuleFor(p => p.ProductTag, f => f.PickRandom<ProductTag>());

        var tags = tagFaker.Generate(100);

        _context.Tags.AddRange(tags);
        _context.SaveChanges();

        //Product_Tag
        var productTagFaker = new Faker<Product_Tag>();

        var products_tags = productTagFaker.Generate(200);

        //Products
        var prodcutNames = new[] { "Piano", "Laptop", "Computer", "Table", "Chair", "Doll", "Ring", "Necklace", "Sword", "Notebook", "Clock", "Painting", "Door", "Balloon", "Shoes" };

        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.PickRandom(prodcutNames))
            .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(50, 1000), 2));

        var products = productFaker.Generate(100);

        foreach (var product in products)
            product.Reviews.AddRange(reviews.SkipLast(5 * (products.IndexOf(product) - 1)).TakeLast(5));

        foreach (var product_tag in products_tags)
        {
            int index = products_tags.IndexOf(product_tag);
            product_tag.Tag = tags.Skip(index < 100 ? index % 100 : (index + 3) % 100).First();
            product_tag.Product = products.Skip(index % 100).First();
        }

        _context.ProductTags.AddRange(products_tags);
        _context.SaveChanges();

        //Payments
        var paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Status, f => f.PickRandom<Status>())
            .RuleFor(p => p.Discount, f => Math.Round(f.Random.Decimal(), 2))
            .RuleFor(p => p.Deadline, f => f.Date.Soon(60, DateTime.Now));

        var payments = paymentFaker.Generate(100);

        _context.Payments.AddRange(payments);
        _context.SaveChanges();

        //Orders
        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.Status, f => f.PickRandom<Status>())
            .RuleFor(o => o.Amount, f => f.Random.Int(1, 300))
            .RuleFor(o => o.Deadline, f => f.Date.Soon(60, DateTime.Now))
            .RuleFor(o => o.Payment, f => { f.IndexVariable++; return payments[f.IndexVariable - 1]; });

        var orders = orderFaker.Generate(100);

        _context.Orders.AddRange(orders);
        _context.SaveChanges();

        //Shops
        var shopFaker = new Faker<Shop>()
            .RuleFor(p => p.Name, f => f.Name.Random.Word())
            .RuleFor(p => p.Description, f => f.Random.Words(5))
            .RuleFor(p => p.Address, f => f.PickRandom(addresses));

        var shops = shopFaker.Generate(5);

        _context.Shops.AddRange(shops);

        foreach (var shop in shops)
            shop.Employees.AddRange(employees.Skip(20 * shops.IndexOf(shop)).Take(20));

        _context.SaveChanges();

        //Roles
        //Role are static data, they were inserted during migrations

        //ProductAmounts
        var productAmountFaker = new Faker<Product_Amount>()
            .RuleFor(p => p.Amount, f => f.Random.Int(1, 30));

        var productAmounts = productAmountFaker.Generate(100);

        foreach (var productAmount in productAmounts)
        {
            int index = productAmounts.IndexOf(productAmount);
            productAmount.Shop = shops.Skip(index % 5).First();
            productAmount.Product = products.Skip(index % 100).First();
        }

        _context.ProductAmounts.AddRange(productAmounts);
        _context.SaveChanges();

        foreach (var order in orders)
        {
            order.Product = products.Skip(orders.IndexOf(order)).First();
            order.Shop = order.Product.Shops.First(s => s.Products.Contains(order.Product));
            order.Customer = customers.Skip(orders.IndexOf(order)).First();
        }

        foreach (var payment in payments)
            payment.Total = Helpers.CalculateTotal(payment.Order);

        _context.SaveChanges();
    }
}