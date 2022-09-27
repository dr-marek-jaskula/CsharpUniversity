# OnDeleteClient

In order to obtain the DeleteCascade not in the database, but in the memory (what will be reflected in the database), in the model configuration part we can use:

```csharp
.OnDelete(DeleteBehavior.ClientCascade);
```

This approach will not result in any changes to the database.
Therefore, no migration needs to be applied.

For entities being tracked by the DbContext, dependent entities will be deleted when the related principal is deleted.

We can also use "ClientSetNull" or "ClientNoAction".
