# ValueGenerated

- ValueGeneratedOnAddOrUpdate approach: the default value will be applied when the change is done to the certain record (good for "UpdateDate")

```csharp
builder.Property(u => u.UpdatedDate)
    //Generate the value when the update is made and when data is added
    .ValueGeneratedOnAddOrUpdate()
    .HasDefaultValueSql("getutcdate()") 
    .HasColumnType("DATETIME2");
```

Use HasDefaultValueSql with "getutcdate", because it needs to be the sql command.
