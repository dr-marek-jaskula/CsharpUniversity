# QueryPerformance

The performance of written queries vary from developer to developer.

Therefore, it is important to keep up to some general principles, but in special cases use the specific approach.

## 1. Move calculations to the database

Let SQL do most of the work.

For this purpose, move "ToList" or "First" method to the end of the query if possible.

Use IQueryable if possible (see CsharpAdvaced -> Linq -> IQueryableIEnumerableIList).

```csharp
//Bad example
var dollsAndTablesBadExample = _context.Products.ToList().Where(p => p.Name == "Doll" || p.Name == "Table");

//Good example
var dollsAndTablesGoodExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").ToList();

//Good example with IQueryable:
IQueryable<Employee> employees = _context.Employees.Where(emp => EF.Functions.Like(emp.FirstName, "J%"));
var result = employees.Take(2);
```

The final SQL query will be similar to:
```sql
SELECT TOP (2) * FROM [Employees] WHERE [FirstName] LIKE 'J%';
```

## 2. No not load data that you do not need

Use Select() to pick data you need:

```csharp
//Assume we need just a prices of dolls and tables:

//Bad example
var dollsAndTablesBadExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").ToList();

//Good example
var dollsAndTablesPricesGoodExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").Select(p => p.Price).ToList();
```

## 3. Add AsNoTracking if possible 

See file "Tracking.md".

## 4. Do not include the relationships, but pick what you need from the relationships

```csharp
//Assume we need just products and their reviews in shops

//Bad example
var productsWithAmountsandShopsBadExample = _context.Products
    .Include(product => product.Shops)
    .Include(product => product.Reviews)
    .ToList(); //comment this one to examine the differences

//Good example
var productsWithAmountsandShopsGoodExample = _context.Products
    .Include(product => product.Reviews)
    .ToList();
```

## 5. Add indexes for every property that you will use for filtering or sorting

About Indexes:

- Indexes are like "Table of Contents".
- Therefore, they speed up querying (SELECT statements)
- However, they slow down the DML (Data Manipulation Language -> Insert, Update, Delete), because changes needs to be reflected in index (additional operation)

```csharp
entity.HasIndex(x => x.PublishedOn);
entity.HasIndex(x => x.ActualPrice);
entity.HasIndex(x => x.ReviewsAverageVotes);
entity.HasIndex(x => x.SoftDeleted);
```

Basically, use the indexes massively when you mostly query data from the database (or just query).

Be careful when using them when you insert, update or delete data from database.

Use indexes on columns that are commonly used for filtering and sorting (and for similar operations).

More about Indexes in: IndexesPerformance.md

## 6. Use "Find" method if the search criteria is primary key. 

Find method is faster and can be used just for DbSet<Type> and primary key.

```csharp
//Bad example
var productsFirstOrDefaultBadExample = _context.Products
    .FirstOrDefault(p => p.Id == 2); 

//Good example
var productsFindGoodExample = _context.Products
    .Find(2);
```

## 7. We should query just the data we need

If we map data to the dto object that does not require all data we queried, we have performance issue.

For AutoMapper we should use "ProjectTo" method to avoid this issue (see "AddressService -> GetById"):

```csharp
var result = _mapper.ProjectTo<AddressDto>(address).FirstOrDefault(); 
```

## 8. Use AsSplitQuery method to split queries when making includes. 

This can help to deal with duplicated data so to avoid the cartesian explosion problem.

Nevertheless, there are many cases when AsSplitQuery can create performance issue, so be careful.

For more info see "ASP.NETCoreWebAPI" ReadMe, section "Entity Framework Core topics" ("OrderService" -> "GetById").

```csharp
//Duplicated data (with explicit loading)
var order = _context.Orders
    .Find(1);

if (order is null)
    throw new Exception("Order not found");

_context.Entry(order).Reference(o => o.Product).Query()
    .Include(p => p.Shops)
    .Include(p => p.Tags)
    .Load();

_context.Entry(order).Reference(o => o.Payment).Query()
    .Load();

//No duplicated data. But be careful -> When using split queries with Skip/Take, pay special attention to making your query ordering fully unique; not doing so could cause incorrect data to be returned
var order2 = _context.Orders
    .Find(1);

if (order is null)
    throw new Exception("Order not found");

_context.Entry(order).Reference(o => o.Product).Query()
    .Include(p => p.Shops)
    .Include(p => p.Tags)
    .AsSplitQuery() //Splitting queries can be used to avoid the cartesian explosion problem
    .Load();

_context.Entry(order).Reference(o => o.Payment).Query()
    .AsSplitQuery()
    .Load();
```

## Other Tips

- In LINQ, we use contains method for checking existence. It is converted to "WHERE IN" in SQL which cause performance degrades. It is better to use "Like" method from DBFunction library.
- Views degrade the LINQ query performance costly. These are slow in performance and impact the performance greatly. So avoid using views in LINQ to Entities.
- When we are binding data to grid or doing paging, retrieve only required number of records to improve performance. This can achieved by using Take and Skip methods.

- Use "DistinctBy":

```csharp
//Simple
_context.Users.GroupBy(u => u.CreatedDate).Select(gru => gru.First());

//Fast and simple, available in .NET 6
_context.Users.DistinctBy(u => u.CreatedDate);
```