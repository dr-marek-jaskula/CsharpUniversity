using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Review
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Stars { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Product? Product { get; set; }
    public int? ProductId { get; set; }
    public Employee? Employee { get; set; }
    public int? EmployeeId { get; set; }
}

public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("review");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).UseIdentityColumn();

        builder.Property(r => r.UserName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(s => s.Stars)
            .IsRequired(true)
            .HasColumnType("TINYINT");

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId);
        
        builder.HasOne(r => r.Employee)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.EmployeeId);
    }
}
