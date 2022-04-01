using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCore.EF_Core_advance;

public class Tracking
{

    //It is important to examine how the Entity Framework Core works in terms of tracking the records

    //EFCore provide the system of Change Tracking, and the changes are applied when the SaveChanged is called

    //Entities are track when:
    //1. Returned from a query executed against the database
    //2. Explicitly attached to the DbContext by Add, Attach, Update or similar methods
    //3. Detected as new entities connected to existing tracked entities

    //Entity instances are no longer tracked when:
    //1. DbContext is disposed
    //2. The change tracker is cleared
    //3. The entities are explicitly detached

    //Entity states:
    //1. Detached - entities are not being tracked by DbContext
    //2. Added - entities are new and have not yet been inserted into the database -> they will be inserted when SaveChanges is called
    //3. Unchanged - entities have not been changed since they were queried from database.
    //4. Modified - entities have been changed since they were queried from the database. They will be updated when SaveChanges is called
    //5. Deleted - entities exists in the database, but are mark to be deleted when SaveCahnges is called

    //As a consequence, the tracked entity is stored twice: before modification and after (this is memory consuming)

    //Therefore, when they entities should be readonly, or the modifications should not be reflected in the database we use:
    //AsNoTracking() method, to inform that entity should not be tracked.

    //!! Use AsNoTracking wherever you can

    private readonly MyDbContext _context;

    public Tracking(MyDbContext context)
    {
        _context = context;
    }

    #region AsNoTracking

    public void QueryProduct()
    {
        //changes on "products" will be reflected in the database after SaveChanges
        var products = _context.Products.Where(p => p.Price > 900);

        products.ToList().ForEach(p => p.Price--);
        _context.ChangeTracker.DetectChanges();
        Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);
    }

    public void QueryProductAsNoTracking()
    {
        //changes on "products" will not be reflected in the database after SaveChanges
        var products = _context.Products.AsNoTracking().Where(p => p.Price > 900);

        products.ToList().ForEach(p => p.Price--);
        _context.ChangeTracker.DetectChanges();
        Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);
    }

    #endregion
}

