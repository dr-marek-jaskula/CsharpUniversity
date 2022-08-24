using Microsoft.EntityFrameworkCore;

namespace EFCore.EF_Core_advance;

public class CancellationTokenInAsyncQueries
{
    private readonly MyDbContext _context;

    public CancellationTokenInAsyncQueries(MyDbContext context)
    {
        _context = context;
    }

    //Very important aspect of querying data from the database is to pass the cancellation token to the async method like "ToListAsync"
    //This will result in canceling the query in progress when it is needed.
    //Without it, even if the client cancels the request, the query is not canceled and the database is busy

    //It can help for instance if the system is under attack and we want to cancel the request. 

    //This is VERY IMPORTANT!

    public void CancellationTokenExample(CancellationToken token)
    {
        //So when the cancellation is made, the query is stopped!
        _context.Addresses
            .ToListAsync(token);
    }
}

