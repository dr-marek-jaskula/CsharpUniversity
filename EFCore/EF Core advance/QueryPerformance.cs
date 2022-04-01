using Microsoft.EntityFrameworkCore;

namespace EFCore.EF_Core_advance;

public class QueryPerformance
{
    //The performance of written queries vary from developer to developer.
    //Therefore, it is important to keep up to some general principle, but is special cases use the specific approach.

    public readonly MyDbContext _context;

    public QueryPerformance(MyDbContext context)
    {
        _context = context;
    }

    //Principles:
    //1. Move the calculation to the database (let sql do most of the work). For this purpose write LINQ queries that are translated to sql => move "ToList" to the end of the query if possible
    public void FirstPrincipleExample()
    {
        //Bad example
        var dollsAndTablesBadExample = _context.Products.ToList().Where(p => p.Name == "Doll" || p.Name == "Table");

        //Good example
        var dollsAndTablesGoodExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").ToList();
    }

    //2. Do not load data that you do not need -> use Select() to pick data you need
    public void SecondPrincipleExample()
    {
        //Assume we need just a prices of dolls and tables:

        //Bad example
        var dollsAndTablesBadExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").ToList();

        //Good example
        var dollsAndTablesPricesGoodExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").Select(p => p.Price).ToList();
    }

    //3. Add AsNoTracking if possible (go to "Tracking")

    //4. Do not include the relationships but pick what you need from the relationships
    public void FourthPrincipleExample()
    {
        //Assume we need just products and amounts of then in shops
        //Bad example
        var productsWithAmountsandShopsBadExample = _context.Products
            .Include(product => product.ProductAmounts)
                .ThenInclude(productAmount => productAmount.Shop)
            .Include(product => product.Product_Tags).
                ThenInclude(productTags => productTags.Tag)
            .ToList(); //comment this one to examine the differences

        //Good example
        var productsWithAmountsandShopsGoodExample = _context.Products
            .Include(product => product.ProductAmounts)
                .ThenInclude(productAmount => productAmount.Shop)
            .ToList();
    }

    //5. Add indexes for every property that you will use for filtering or sorting
    //https://github.com/JonPSmith/EfCoreinAction-SecondEdition/blob/Part3/BookApp.Persistance.EfCoreSql.Books/Configurations/BookConfig.cs#L14L17


}

