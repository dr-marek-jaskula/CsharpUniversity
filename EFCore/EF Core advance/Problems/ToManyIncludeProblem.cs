namespace EFCore.EF_Core_advance.Problems;

public class ToManyIncludeProblem
{
    //Avoid using too many Include's when queering data form database

    #region Example of a problem

    /*
    app.MapGet("getOrderDetails", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db);
        return new { OrderId = order.OrderId, Details = order.OrderDetails };
    });

    app.MapGet("getOrderWithShipper", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db);
        return new { OrderId = order.OrderId, ShipVia = order.ShipVia, Shipper = order.ShipViaNavigation };
    });

    app.MapGet("getOrderWithCustomer", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db);
        return new { OrderId = order.OrderId, Customer = order.Customer };
    });

    app.Run();

    async Task<Order> GetOrder(int orderId, NorthwindContext db, IEnumerable<Expression<Func<Order, object>>> includes)
    {
        //Too many includes (for some use cases the included values are not necessary, so it is not efficient)
        Order order = await db.Orders
            .Include(o => o.OrderDetails)
            .Include(o => o.ShipViaNavigation)
            .Include(o => o.Customer)
            .FirstAsync(o => o.OrderId == orderId);

        return order;
    }
    */

    #endregion Example of a problem

    #region Solution

    /*
    app.MapGet("getOrderDetails", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db, o => o.OrderDetails);
        return new { OrderId = order.OrderId, Details = order.OrderDetails };
    });

    app.MapGet("getOrderWithShipper", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db, o => o.ShipViaNavigation);
        return new { OrderId = order.OrderId, ShipVia = order.ShipVia, Shipper = order.ShipViaNavigation };
    });

    app.MapGet("getOrderWithCustomer", async (NorthwindContext db) =>
    {
        Order order = await GetOrder(10252, db, o => o.Customer);
        return new { OrderId = order.OrderId, Customer = order.Customer };
    });

    app.Run();

    //Here we have boxing with object
    async Task<Order> GetOrder(int orderId, NorthwindContext db, params Expression<Func<Order, object>>[] includes)
    {
        //Too many includes (for some use cases the included values are not necessary, so it is not efficient)
        //Order order = await db.Orders
        //    .Include(o => o.OrderDetails)
        //    .Include(o => o.ShipViaNavigation)
        //    .Include(o => o.Customer)
        //    .FirstAsync(o => o.OrderId == orderId);

        //Solution
        var baseQuery = db.Orders
            .Where(o => o.OrderId == orderId);
        //.AsQueryable(); //if there is no method that will make is IQueryable

        if (includes.Any())
            foreach (var include in includes)
                baseQuery = baseQuery.Include(include);

        Order order = await baseQuery.FirstAsync();

        return order;
    }
    */

    #endregion Solution
}