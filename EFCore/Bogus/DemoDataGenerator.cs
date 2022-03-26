using Bogus;
using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Bogus;

public class DemoDataGenerator
{
    private readonly MyDbContext _context;

    public DemoDataGenerator(MyDbContext context)
    {
        _context = context;
    }

    public async void Generate()
    {
        //This Faker class is from the Bogus NuGet package
        #region Fake data

        //Addresses
        var addressFaker = new Faker<Address>()
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Building, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 30) : null) //Bool(0.9f) means give me 90% time true and 10% false
            .RuleFor(a => a.Flat, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 90) : null);

        var addresses = addressFaker.Generate(100); //this generates 100 random addresses.

        //SalaryTransfers
        var salaryTransferFaker = new Faker<SalaryTransfer>()
            .RuleFor(p => p.IsIncentiveBonus, f => f.Random.Bool())
            .RuleFor(p => p.IsTaskBonus, f => f.Random.Bool())
            .RuleFor(p => p.IsDiscretionaryBonus, f => f.Random.Bool())
            .RuleFor(p => p.Date, f => f.Date.Between(DateTime.Now - new TimeSpan(180, 3, 0,0,0), DateTime.Now));

        var salaryTransfers = salaryTransferFaker.Generate(5000);

        //Salary
        var salaryFaker = new Faker<Salary>()
            .RuleFor(p => p.BaseSalary, f => f.Random.Int(2000, 5000))
            .RuleFor(p => p.DiscretionaryBonus, f => f.Random.Decimal() * f.Random.Int(100, 500))
            .RuleFor(p => p.IncentiveBonus, f => f.Random.Decimal() * f.Random.Int(100, 500))
            .RuleFor(p => p.TaskBonus, f => f.Random.Decimal() * f.Random.Int(100, 500))
            .RuleFor(p => p.SalaryTransfer, f => salaryTransfers.Skip(f.Random.Int(0,4900)).Take(20).ToList());

        var salaries = salaryFaker.Generate(100);

        //Reviews
        var reviewTitles = new[] { "So bad...", "Great!", "Not bad", "Be careful", "I like", "Good", "Ehhh...", "???", "!!!" };

        var reviewsFaker = new Faker<Review>()
            .RuleFor(p => p.Stars, f => f.Random.Int(0, 5))
            .RuleFor(p => p.UserName, f => f.Name.Random.Word())
            .RuleFor(p => p.Title, f => f.PickRandom(reviewTitles));

        var reviews = reviewsFaker.Generate(1000);

        //Employees
        var employeeFaker = new Faker<Employee>()
            .RuleFor(e => e.Gender, f => f.PickRandom<Gender>())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            //.RuleFor(e => e.FirstName, (f, e) => f.Name.FirstName(e.Gender))
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.DateOfBirth, f => f.Date.Past(23, DateTime.Now))
            .RuleFor(e => e.HireDate, f => f.Date.Past(3, DateTime.Now))
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.ContactNumber, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.Address, f => f.Random.Bool(0.9f) ? f.PickRandom(addresses) : null)
            .RuleFor(e => e.Salary, f => f.PickRandom(salaries))
            .RuleFor(e => e.Reviews, f => reviews.Skip(f.Random.Int(0,980)).Take(f.Random.Int(1,4)).ToList());

        var employees = employeeFaker.Generate(100);

        //Customers
        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past(23, DateTime.Now))
            .RuleFor(c => c.Rank, f => f.PickRandom<Rank>())
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.ContactNumber, f => f.Phone.PhoneNumber())
            .RuleFor(c => c.Address, f => f.Random.Bool(0.1f) ? f.PickRandom(addresses) : null);

        var customers = customerFaker.Generate(400);

        //Tags
        var tagFaker = new Faker<Tag>()
            .RuleFor(p => p.ProductTag, f => f.PickRandom<ProductTag>());

        var tags = tagFaker.Generate(100);

        //ProductAmounts
        var productAmountFaker = new Faker<ProductAmount>()
            .RuleFor(p => p.Amount, f => f.Random.Int(1, 30));

        var productAmounts = productAmountFaker.Generate(100);

        //Products
        var prodcutNames = new[] { "Piano", "Laptop", "Computer", "Table", "Chair", "Doll", "Ring", "Necklace", "Sword", "Notebook", "Clock", "Painting", "Door", "Balloon", "Shoes" };

        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.PickRandom(prodcutNames))
            .RuleFor(p => p.Price, f => f.Random.Decimal(0.1m, 1) * f.Random.Int(50, 1000))
            .RuleFor(p => p.Reviews, f => reviews.Skip(f.Random.Int(0, 980)).Take(f.Random.Int(2, 5)).ToList());

        var products = productFaker.Generate(100);

        //Product_Tag
        var productTagFaker = new Faker<Product_Tag>()
            .RuleFor(p => p.Tag, f => f.PickRandom(tags));

        var products_tags = productTagFaker.Generate(500);

        //Payments
        var paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Status, f => f.PickRandom<Status>())
            .RuleFor(p => p.Discount, f => f.Random.Decimal())
            .RuleFor(p => p.Deadline, f => f.Date.Soon(60, DateTime.Now));

        var payments = paymentFaker.Generate(100);

        //Orders
        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.Status, f => f.PickRandom<Status>())
            .RuleFor(o => o.Amount, f => f.Random.Int(1, 300))
            .RuleFor(o => o.Deadline, f => f.Date.Soon(60, DateTime.Now))
            .RuleFor(o => o.Payment, f => payments[f.IndexFaker]);

        var orders = orderFaker.Generate(100);

        //Shops
        var shopFaker = new Faker<Shop>()
            .RuleFor(p => p.Name, f => f.Name.Random.Word())
            .RuleFor(p => p.Address, f => f.PickRandom(addresses));

        var shops = shopFaker.Generate(5);

        #endregion

        #region Add related data

        foreach (var shop in shops)
            shop.Employees.AddRange(employees.Skip(20 * shops.IndexOf(shop)).Take(20));

        foreach (var employee in employees)
            employee.Customers.AddRange(customers.Skip(4 * employees.IndexOf(employee)).Take(4));

        foreach (var product_tag in products_tags)
        {
            int index = products_tags.IndexOf(product_tag);
            product_tag.Tag = tags.Skip(index % 80).First();
            product_tag.Product = products.Skip(index % 100).First();
        }

        foreach (var productAmount in productAmounts)
        {
            int index = productAmounts.IndexOf(productAmount);
            productAmount.Shop = shops.Skip(index % 5).First();
            productAmount.Product = products.Skip(index % 100).First();
        }
        
        foreach (var customer in customers)
        {
            int index = customers.IndexOf(customer);
            customer.Orders.AddRange(orders.Skip(4 * index).Take(4));
            if (index == 25)
                break;
        }

        foreach (var order in orders)
        {
            int index = orders.IndexOf(order);
            order.Product = products.Skip(index % 100).First();
            order.Shop = shops.Skip(index % 5).First();
            order.Payment = payments.Skip(index % 100).First();
            order.Payment.Total = Helpers.CalculateTotal(order);
        }

        #endregion

        #region Add to db context sets

        //Add to db context sets
        _context.Shops.AddRange(shops);
        _context.Customers.AddRange(customers); 
        _context.Employees.AddRange(employees);

        _context.Products.AddRange(products);
        _context.Orders.AddRange(orders);

        #endregion

        //Insert fake data into the database
        await _context.SaveChangesAsync(); 
    }

    public async Task ClearAll()
    {
        await _context.Database.ExecuteSqlRawAsync(@$"
            DELETE FROM [{nameof(MyDbContext.Addresses)}];
            DELETE FROM [{nameof(MyDbContext.Customers)}];
            DELETE FROM [{nameof(MyDbContext.Employees)}];
            DELETE FROM [{nameof(MyDbContext.Orders)}];
            DELETE FROM [{nameof(MyDbContext.Payments)}];
            DELETE FROM [{nameof(MyDbContext.Products)}];
            DELETE FROM [{nameof(MyDbContext.Products_Tag)}];
            DELETE FROM [{nameof(MyDbContext.ProductAmounts)}];
            DELETE FROM [{nameof(MyDbContext.Reviews)}];
            DELETE FROM [{nameof(MyDbContext.Salaries)}];
            DELETE FROM [{nameof(MyDbContext.Shops)}];
            DELETE FROM [{nameof(MyDbContext.Tags)}];
        ");

        _context.ChangeTracker.Clear();
    }
}

