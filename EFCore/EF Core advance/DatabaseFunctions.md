# DatabaseFunctions

There are some cases when Entity Framework Core does not translate the LINQ query to SQL in way we want to. 

For this rare situations we can use the DbFunctions class.

When filtering entities by date only, we would call ".Date" on our DateTime property -> but it is not a valid LINQ-to-Entities expression and it's unable to execute against DB.
Therefore, we can use DbFunction class with its helpers classes to perform a valid LINQ-to-Entity expression.
It is better than using chain method (Contains etc. we can use a single like)

```csharp
/// <summary>
/// Returns a list of order with deadline equal to the next day
/// </summary>
/// <returns></returns>
public IEnumerable<Order> GetSomeOrders()
{
   DbFunctions dbFunctions = EF.Functions;
   return _context.Orders.Where(o => dbFunctions.Like(o.Status.ToString(), "%ed%"));
}
```
