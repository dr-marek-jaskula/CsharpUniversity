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