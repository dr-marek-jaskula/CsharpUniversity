using Bogus;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;

namespace EFCore.EF_Core_advance;

public class BulkInsert
{
    //Bulk Insert called also "Bulk Copy" is the performant way to insert a large number of records

    //The way to achieve Bulk Insert is to use linq2db.EntityFrameworkCore NuGet Package
    //(or use GenerateAndInsertWithSqlCopy – with SqlBulkCopy class implementation, but it is not so readable, use only in very hight performance scenarios)
    private readonly MyDbContext _context;

    public BulkInsert(MyDbContext context)
    {
        _context = context;
    }

    public async Task BulkInsertExample()
    {
        var addressFaker = new Faker<EFCore.Data_models.Address>()
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Building, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 30) : null) 
            .RuleFor(a => a.Flat, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 90) : null);

        var addresses = addressFaker.Generate(10001);

        var options = new BulkCopyOptions
        {
            TableName = "Address"
        };

        await _context.BulkCopyAsync(options, addresses);
    }
}

