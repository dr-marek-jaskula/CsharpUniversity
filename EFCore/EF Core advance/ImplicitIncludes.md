# Implicit Includes

Rather then use the explicit includes like this:

```csharp
var query = _context.People
    .TagWith("Explicit Include: Query the person with his or her address and id 1")
    .Include(p => p.Address)
    .Where(p => p.Id == 1);
```

We can use the implicit includes, to get just the part of the related data we need:

```csharp
var betterQuery = _context.People
    .TagWith("Implicit Include: Query the person with his or her address and id 1")
    .Where(p => p.Id == 1)
    .Select(x => new
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName,
        Street = x.Address.Street,
        City = x.Address.City //and so on
    }); 
```
By this the performance can be improved.

The other way to obtain this behavior, when using AutoMapper, is to use the "ProjectTo" method (see the QueryPerformance or Address service GetById method).