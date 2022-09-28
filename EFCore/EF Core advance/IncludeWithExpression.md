# IncludeWithExpression

If there are many different endpoints that are based on a single helper method, then may happen that the method will contain many includes.

To avoid this problem and have generic solution we can pass "Expressions" as a parameters ("params") to determine what includes should be taken into account.

```csharp
public async Task<Order> GetOrder(int orderId, MyDbContext db, params Expression<Func<Order, object?>>[] includes)
{
    //Too many includes (for some use cases the included values are not necessary, so it is not efficient)
    //Order order = await db.Orders
    //    .Include(o => o.Payment)
    //    .Include(o => o.Customer)
    //    .Include(o => o.Product)
    //    .FirstAsync(o => o.Id == orderId);

    //Solution: create a IQueryable object
    var baseQuery = db.Orders
        .Where(o => o.Id == orderId);
    //.AsQueryable(); //if there is no method that will make is IQueryable

    //Include data that are required
    if (includes.Any())
        foreach (var include in includes)
            baseQuery = baseQuery.Include(include);

    //Materialize the result
    Order order = await baseQuery.FirstAsync();

    return order;
}
```

Then, we can use it in the following manner:

```csharp
public async Task<(int OrderId, Payment? Payment)> GetOrderPayment(MyDbContext db)
{
    Order order = await GetOrder(4, db, o => o.Payment);
    return (order.Id, order.Payment);
}

public async Task<(int OrderId, Customer? Customer)> GetOrderCustomer(MyDbContext db)
{
    Order order = await GetOrder(4, db, o => o.Customer);
    return (order.Id, order.Customer);
}

public async Task<(int OrderId, Product? Product)> GetOrderProduct(MyDbContext db)
{
    Order order = await GetOrder(4, db, o => o.Product);
    return (order.Id, order.Product);
}
```