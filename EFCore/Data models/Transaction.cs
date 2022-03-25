using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Transaction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; } 
    public Order Order { get; set; } = new();
    public int OrderId { get; set; }
    public Customer Customer { get; set; } = new();
    public int CustomerId { get; set; }
}

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transaction");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).UseIdentityColumn();

        builder.Property(t => t.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .HasMaxLength(200);

        builder.Property(t => t.Date)
            .HasColumnType("DATE");

        builder.HasOne(t => t.Order)
           .WithMany(o => o.Transactions)
           .HasForeignKey(t => t.OrderId);

        builder.HasOne(t => t.Customer)
           .WithMany(o => o.Transactions)
           .HasForeignKey(t => t.CustomerId);
    }
}
