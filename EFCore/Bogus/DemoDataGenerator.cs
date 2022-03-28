using Bogus;
using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.BogusDemo;

public class DemoDataGenerator
{
    private readonly MyDbContext _context;

    private static Bogus.DataSets.Name.Gender ReturnGenderType(Gender gender)
    {
        if (gender is Gender.Male)
            return Bogus.DataSets.Name.Gender.Male;
        else
            return Bogus.DataSets.Name.Gender.Female;
    }

    public DemoDataGenerator(MyDbContext context)
    {
        _context = context;
    }

    public void Generate()
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

        var addresses = addressFaker.Generate(5000); //this generates 5000 random addresses.

        _context.Addresses.AddRange(addresses);
        _context.SaveChanges();

        //SalaryTransfers
        var salaryTransferFaker = new Faker<SalaryTransfer>()
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
            .RuleFor(p => p.SalaryTransfer, f => { f.IndexVariable++; return salaryTransfers.Skip(20 * (f.IndexVariable-1)).Take(20).ToList(); });

        var salaries = salaryFaker.Generate(100);

        _context.Salaries.AddRange(salaries);
        _context.SaveChanges();

        //Reviews
        var reviewTitles = new[] { "So bad...", "Great!", "Not bad", "Be careful", "I like", "Good", "Ehhh...", "???", "!!!" };

        var reviewsFaker = new Faker<Review>()
            .RuleFor(p => p.Stars, f => f.Random.Int(0, 5))
            .RuleFor(p => p.UserName, f => f.Name.Random.Word())
            .RuleFor(p => p.Title, f => f.PickRandom(reviewTitles));

        var reviews = reviewsFaker.Generate(1000);

        _context.Reviews.AddRange(reviews);
        _context.SaveChanges();

        //Employees
        var employeeFaker = new Faker<Employee>()
            .RuleFor(e => e.Gender, f => f.PickRandom<Gender>())
            .RuleFor(e => e.FirstName, (f, e) => f.Name.FirstName(ReturnGenderType(e.Gender)))
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.DateOfBirth, f => f.Date.Past(23, DateTime.Now))
            .RuleFor(e => e.HireDate, f => f.Date.Past(3, DateTime.Now))
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(e => e.ContactNumber, f => f.Phone.PhoneNumber("### ### ###"))
            .RuleFor(e => e.Address, f => f.PickRandom(addresses))
            .RuleFor(e => e.Salary, f => f.PickRandom(salaries))
            .RuleFor(e => e.Reviews, f => { f.IndexVariable++; return reviews.Skip(5 * (f.IndexVariable - 1)).Take(5).ToList(); });

        var employees = employeeFaker.Generate(100);

        _context.Employees.AddRange(employees);
        _context.SaveChanges();

        //Customers
        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.FirstName, (f, e) => f.Name.FirstName(ReturnGenderType(e.Gender)))
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past(23, DateTime.Now))
            .RuleFor(c => c.Rank, f => f.PickRandom<Rank>())
            .RuleFor(c => c.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(c => c.ContactNumber, f => f.Phone.PhoneNumber("### ### ###"))
            .RuleFor(c => c.Address, f => f.PickRandom(addresses));

        var customers = customerFaker.Generate(400);

        _context.Customers.AddRange(customers);
        _context.SaveChangesAsync();

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
            .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(0.1m, 1) * f.Random.Int(50, 1000), 2))
            .RuleFor(p => p.Reviews, f => reviews.Skip(f.Random.Int(0, 980)).Take(f.Random.Int(2, 5)).ToList());

        var products = productFaker.Generate(100);

        //Product_Tag
        var productTagFaker = new Faker<Product_Tag>()
            .RuleFor(p => p.Tag, f => f.PickRandom(tags));

        var products_tags = productTagFaker.Generate(500);

        //Payments
        var paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Status, f => f.PickRandom<Status>())
            .RuleFor(p => p.Discount, f => f.Random.Int(0, 100)/100)
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
        
        //Put all into shops
        foreach (var shop in shops)
        {
            int index = shops.IndexOf(shop);

            shop.ProductAmounts.AddRange(productAmounts.Where(p => p.Shop == shop));
            shop.ProductAmounts.ForEach(p => p.Product.Product_Tags.AddRange(products_tags.Where(k => k.Product == p.Product)));
        }

        foreach (var customer in customers)
        {
            int index = customers.IndexOf(customer);
            customer.Orders.AddRange(orders.Skip(4 * index).Take(4));

            customer.Orders.ForEach(o =>
            {
                o.Shop = shops.First(s => s.Employees.Any(emp => emp.Customers.Contains(customer))); 
                Random random = new();
                o.Product = o.Shop.ProductAmounts.ElementAt(random.Next(0, o.Shop.ProductAmounts.Count())).Product;
                o.Payment.Total = Helpers.CalculateTotal(o);
            });

            if (index == 25)
                break;
        }

        #endregion

        #region Add to db context sets

        //_context.Employees.AddRange(employees);
        _context.SaveChanges();
        _context.Customers.AddRange(customers);
        _context.Addresses.AddRange(addresses);
        _context.Orders.AddRange(orders);
        _context.Payments.AddRange(payments);
        _context.ProductAmounts.AddRange(productAmounts);
        _context.Products_Tag.AddRange(products_tags);
        _context.Reviews.AddRange(reviews);
        _context.Salaries.AddRange(salaries);
        _context.SalaryTransfers.AddRange(salaryTransfers);
        _context.Shops.AddRange(shops);
        _context.Tags.AddRange(tags);
        _context.Products.AddRange(products);
        

        //bool resolve = shops[0].Orders[0].Customer == shops[0].Employees[0].Customers[0];

        #endregion

        //Insert fake data into the database
        _context.SaveChanges(); 
    }

    public void ClearDatabase()
    {
        _context.Database.ExecuteSqlRaw(@$"
            DELETE FROM [{nameof(Review)}];
            DELETE FROM [{nameof(Customer)}];
            DELETE FROM [{nameof(Address)}];
            DELETE FROM [{nameof(Employee)}];
            DELETE FROM [{nameof(Order)}];
            DELETE FROM [{nameof(Payment)}];
            DELETE FROM [{nameof(Product)}];
            DELETE FROM [{nameof(Product_Tag)}];
            DELETE FROM [{"Product_Amount"}];
            DELETE FROM [{nameof(Salary)}];
            DELETE FROM [{nameof(Shop)}];
            DELETE FROM [{"Salary_Transfer"}];
            DELETE FROM [{nameof(Tag)}];
        ");

        _context.ChangeTracker.Clear();
    }
}

