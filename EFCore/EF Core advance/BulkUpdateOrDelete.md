# Bulk Update Or Delete

Aim is to avoid sending many, like n+1, commands to the database. 

To solve this problem use "linq2db.EntityFrameworkCore"

The implementation of Bulk Update -> OrderService -> BulkUpdate

Problem:

```csharp
app.MapPut("updateProblem", async (NorthwindContext db) =>
{
    //The problem is we have n+1 operations
    //The solution for this problem is to use raw sql or use the NuGet Package "linq2db.EntityFrameworkCore"
    var employees = db.Employees
        .Where(e => e.HireDate > new DateTime(1994, 1, 1))
        .ToList();

    foreach (var user in employees)
        user.Notes = "New employee";

    await db.SaveChangesAsync();
    //n+1 commands will be used
});
```

Solution:

```csharp
app.MapPut("bulkUpdateLinq2db", async (NorthwindContext db) =>
{
    //1) Install NuGet Package "linq2db.EntityFrameworkCore"
    //2) Make IQueryable
    var employees = db.Employees
        .Where(e => e.HireDate > new DateTime(1994, 1, 1));

    //Use LinqToDB
    await LinqToDB.LinqExtensions.UpdateAsync(employees.ToLinqToDB(), x => new Employee
    {
        Notes = "New employee"
    });
    //We do not need to call db.SaveChanges() because Linq to db will use it

    //Just one command will be used
});
```

BulkUpdate by .NET 7
The implementation of Bulk Update -> OrderService -> BulkUpdateDotNet7

```csharp
//Bulk Update by .Net 7 
//IMPORTANT: when using ExecuteUpdateAsync the change tracked does not track the update changes
public void BulkUpdateDotNet7(int addAmount)
{
    //The SaveChangesAsync is executed when ExecuteUpdateAsync is called
    var orders = _dbContext.Orders
        .Where(o => o.Status == Status.InProgress)
        .ExecuteUpdateAsync(
            o => o.SetProperty(p => p.Amount, p => p.Amount + addAmount));
}
```

BulkDelete with .NET 7
The implementation of Bulk Update -> OrderService -> BulkDeleteWithoutQueryingDotNet7

```csharp
//Delete a record or records without querying it with .NET 7
//IMPORTANT: when using ExecuteDeleteAsync the change tracked does not track the update changes
public async void BulkDeleteWithoutQueryingDotNet7(int id)
{
    //The SaveChangesAsync is executed when ExecuteUpdateAsync is called
    await _dbContext.Orders
        .Where(o => o.Id == id)
        .ExecuteDeleteAsync();
}
```