# Tracking

It is important to examine how the Entity Framework Core works in terms of tracking the records.

EFCore provides the system of Change Tracking. Changes are applied when the **SaveChanged** is called.

Entities are track when:

- returned from a query executed against the database
- explicitly attached to the DbContext by Add, Attach, Update or similar methods
- detected as new entities connected to existing tracked entities

Entity instances are no longer tracked when:

- dbContext is disposed
- the change tracker is cleared
- the entities are explicitly detached

Entity states:

- Detached
  - entities are not being tracked by DbContext
- Added
  - entities are new and have not yet been inserted into the database -> they will be inserted when SaveChanges is called
- Unchanged
  - entities have not been changed since they were queried from database.
- Modified
  - entities have been changed since they were queried from the database. They will be updated when SaveChanges is called
- Deleted
  - entities exists in the database, but are mark to be deleted when SaveCahnges is called

As a consequence, the tracked entity is stored twice: 

- before modification 
- after modification

This can be a significant performance issue in some situation. To solve this problem the "AsNoTracking" method was introduced. This method results in not tracking the entity.
Nevertheless, we should use it only when queried entities as meant to be readonly, or the modifications should not be reflected in the database.

No the general advice is to use "AsNoTracking" wherever we can.

Example: 
```csharp
//changes on "products" will be reflected in the database after SaveChanges
var products = _context.Products.Where(p => p.Price > 900);

products.ToList().ForEach(p => p.Price--);

_context.ChangeTracker.DetectChanges();
Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);
```

```csharp
//changes on "products" will not be reflected in the database after SaveChanges
var products = _context.Products.AsNoTracking().Where(p => p.Price > 900);

products.ToList().ForEach(p => p.Price--);
_context.ChangeTracker.DetectChanges();
Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);
```

We can also disable change tracking mechanism on the whole dbContext by:

```csharp
context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
```
