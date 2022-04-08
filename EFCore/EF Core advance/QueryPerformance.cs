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
    //About Indexes: 
    //a) Indexes are like "Table of Contents".
    //b) Therefore, it speeds up the querying (SELECT statements)
    //c) However, it slows down the DML (Data Manipulation Language -> Insert, Update, Delete), because changes needs to be reflected in index (additional operation)

    /* (where entity is for example 
            entity.HasIndex(x => x.PublishedOn);
            entity.HasIndex(x => x.ActualPrice);
            entity.HasIndex(x => x.ReviewsAverageVotes);
            entity.HasIndex(x => x.SoftDeleted); 
     */
    
    //Basically, use the indexes massively when u mostly query data from the database (or just query).
    //Be careful when using them when u insert, update or delete data from database
    //Use indexes on these columns that are commonly used for filtering and sorting (and for similar operations)

    //MORE about Indexes in: MS SQL Server: Jupiter Notebook
    //More about Indexes in: Indexes.cs
}

