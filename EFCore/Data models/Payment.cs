using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Payment
{
    public int Id { get; set; }
    public decimal? Discount { get; set; }
    public decimal Total { get; set; }
    public Status Status { get; set; }
    public DateTime Deadline { get; set; }
    public Order? Order { get; set; }
}

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payment");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseIdentityColumn();

        builder.Property(p => p.Discount)
            .HasPrecision(3, 2);

        builder.Property(p => p.Total)
            .IsRequired(true)
            .HasPrecision(12, 2);

        builder.Property(p => p.Status)
            .IsRequired(true)
            .HasMaxLength(10)
            .HasConversion(status => status.ToString(),
            s => (Status)Enum.Parse(typeof(Status), s))
            .HasComment("Received, InProgress, Done or Rejected");

        builder.Property(p => p.Deadline)
            .HasColumnType("DATE");

        builder.HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Order>(o => o.PaymentId);
    }
}
