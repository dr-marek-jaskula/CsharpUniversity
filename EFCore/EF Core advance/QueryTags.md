# QueryTags

Tagging queries add just a comment above the query.

It is very helpful in case of debugging the query process.

```csharp
_context.Addresses
    .Where(a => a.Flat < 30)
    .TagWith("This is my tag") 
    .ToList();
```

This will result in "-- This is my tag" above the query.