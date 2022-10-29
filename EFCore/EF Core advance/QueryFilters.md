# Query Filters

Query Filters are filters that are set by default. 

Query Filter are defined for specific entities and they are configured in Configuration class by the use of 
```csharp
HasQueryFilter
```

For instance (this QueryFilter is only here, not in current Order configuration):

```csharp
public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).UseIdentityColumn();

        builder.HasQueryFilter(o => o.Status != Status.Done);

        builder.Property(o => o.Amount)
            .IsRequired(true)
            .HasColumnType("INT");

        builder.Property(o => o.Status)
            .IsRequired(true)
            .HasColumnType("VARCHAR(10)")
            .HasDefaultValue(Status.Received)
            .HasConversion(status => status.ToString(),
             s => (Status)Enum.Parse(typeof(Status), s))
            .HasComment("Received, InProgress, Done or Rejected");

        builder.Property(o => o.Deadline)
            .HasColumnType("DATE");

        builder.HasOne(o => o.Product)
            .WithMany(p => p.Order)
            .HasForeignKey(p => p.ProductId);

        builder.HasOne(o => o.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.CustomerId);

        //Indexes
        builder.HasIndex(o => new { o.Deadline, o.Status }, "IX_Order_Deadline_Status")
            .IncludeProperties(o => new { o.Amount, o.ProductId })
            .HasFilter("Status IN ('Received', 'InProgress')");
    }
}
```

However, if do not want to use a QueryFilter in a specific query, then we can do this by using
```csharp
IgnoreQueryFilters()
```

## Configuring multiple Query Filters

Configuring multiple query filters on the same entity will only apply the last one. If we need more than one condition, we can do that with the logical AND operator (&&).

## Ignoring specific Query Filters

If you need to ignore a specific expression in a Query Filter and leave the rest in place, unfortunately, you can't do that. Only one Query Filter is allowed per entity type.

One solution is calling IgnoreQueryFilters, which will remove the configured Query Filter for that entity type. And then manually apply the condition that you need for that specific query.