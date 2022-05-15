using EFCore.Data_models;
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
    //1. Move the calculation to the database (let sql do most of the work. "Move calculation to the Server side").
    //For this purpose write LINQ queries that are translated to sql => move "ToList" to the end of the query if possible
    //Use IQueryable if possible!! (see CsharpAdvaced -> Linq -> IQueryableIEnumerableIList)
    public void FirstPrincipleExample()
    {
        //Bad example
        var dollsAndTablesBadExample = _context.Products.ToList().Where(p => p.Name == "Doll" || p.Name == "Table");

        //Good example
        var dollsAndTablesGoodExample = _context.Products.Where(p => p.Name == "Doll" || p.Name == "Table").ToList();

        //Good example with IQueryable:
        IQueryable<Employee> employees = _context.Employees.Where(emp => EF.Functions.Like(emp.FirstName, "J%"));
        var result = employees.Take(2);
        //Even it was separated into two rows, the final SQL query will be similar to:
        //SELECT TOP (2) * FROM [Employees] WHERE [FirstName] LIKE 'J%';
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

    //Other tips:

    //a) In LINQ, we use contains method for checking existence. It is converted to "WHERE IN" in SQL which cause performance degrades.

    //b) Views degrade the LINQ query performance costly. These are slow in performance and impact the performance greatly. So avoid using views in LINQ to Entities.

    //c) When we are binding data to grid or doing paging, retrieve only required no of records to improve performance. This can achieved by using Take and Skip methods.
}