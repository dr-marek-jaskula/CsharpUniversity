# RawSql

In order to execute the raw sql on the database, using EF Core we use:

To use raw sql on database, independently from tables:

```csharp
_myDbContext.Database.ExecuteSqlRaw(@"
UPDATE Review
SET UpdatedDate = GETDATE()
WHERE Id = 4;
");
```

In order to use string interpolation in the RawSql and **protect** the application from raw sql injection, we need to use:

```csharp
var valueToInterpolate = "85";
//we can validate the above input
var tags = _myDbContext.Tags
.FromSqlInterpolated($@"
SELECT Tag.Id, Tag.ProductTag
FROM Tag
WHERE Tag.Id > {valueToInterpolate};
")
.ToList();
//The interpolated string will be set as parameter
```
