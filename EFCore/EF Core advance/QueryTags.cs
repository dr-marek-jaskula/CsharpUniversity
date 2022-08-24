using Microsoft.EntityFrameworkCore;

namespace EFCore.EF_Core_advance;

public class QueryTags
{
    //Tagging queries add just a simple comment above the query.
    //It is very helpful in case of debugging the query process and identifying 

    private readonly MyDbContext _context;

    public QueryTags(MyDbContext context)
    {
        _context = context;
    }

    public void TagQueryExample()
    {
        _context.Addresses
            .Where(a => a.Flat < 30)
            .TagWith("This is my tag") //this will result in "-- This is my tag" above the query
            .ToList();
    }
}