# Cancellation Token In Async Queries

Very important aspect when querying data from the database is passing the cancellation token to the async method like to method "ToListAsync".
This will result in canceling the query in progress, when it is needed.

Without it, even if the client cancels the request, the query is not canceled and the database is busy.

It can help for instance if the system is under attack and we want to cancel the request. 

This is *very important*!

```csharp
public void CancellationTokenExample(CancellationToken token)
{
    //So when the cancellation is made, the query is stopped!
    _context.Addresses
        .ToListAsync(token);
}
```