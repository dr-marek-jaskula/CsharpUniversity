# DefaultValues

We can choose two options for inserting the default values in the Entity Framework Core.

- Use "HasDefaultValue" approach:

```csharp
builder.Property(o => o.Status)
    .IsRequired(true)
    .HasColumnType("VARCHAR(10)")
    .HasDefaultValue(Status.Received)
    .HasConversion(status => status.ToString(),
     s => (Status)Enum.Parse(typeof(Status), s))
    .HasComment("Received, InProgress, Done or Rejected");
```

**WARNING**: this will create a DateTime that is equal to the migration DateTime creation!!

```csharp
builder.Property(u => u.CreateTime)
    .HasDefaultValue(DateTime.Now.Date)
    .HasColumnType("DATE");
```

- Use "HasDefaultValueSql" approach:

```csharp
builder.Property(u => u.CreatedDate)
    .HasDefaultValueSql("getutcdate()") 
    .HasColumnType("DATETIME2");
```

We need to use HasDefaultValueSql with "getutcdate", because it needs to be the sql command.
