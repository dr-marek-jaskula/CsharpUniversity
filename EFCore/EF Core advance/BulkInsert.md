# BulkInsert

Bulk Insert, called also "Bulk Copy", is the efficient way to insert a large number of records to the database.

The way to achieve Bulk Insert is to use linq2db.EntityFrameworkCore NuGet Package.

Note: we can use GenerateAndInsertWithSqlCopy – with SqlBulkCopy class implementation, but it is not so readable, use only in very hight performance scenarios.

```csharp
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
```