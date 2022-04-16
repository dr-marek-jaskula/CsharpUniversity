using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Order
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public Status Status { get; set; }
    public DateTime Deadline { get; set; }
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Payment? Payment { get; set; }
    public int? PaymentId { get; set; }
    public virtual Shop? Shop { get; set; }
    public int? ShopId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int? CustomerId { get; set; }
}

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).UseIdentityColumn();

        builder.Property(o => o.Amount)
            .IsRequired(true)
            .HasColumnType("INT");

        builder.Property(o => o.Status)
            .IsRequired(true)
            .HasColumnType("VARCHAR(10)")
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
    }
}