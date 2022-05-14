using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Review
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Stars { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Employee? Employee { get; set; }
    public int? EmployeeId { get; set; }
}

public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).UseIdentityColumn();

        builder.Property(r => r.UserName)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(s => s.Stars)
            .IsRequired(true)
            .HasColumnType("TINYINT");

        builder.Property(u => u.CreatedDate)
            .HasDefaultValueSql("getutcdate()") //need to use HasDefaultValueSql with "getutcdate" because it need to be the sql command
            .HasColumnType("DATETIME2");

        builder.Property(u => u.UpdatedDate)
            .ValueGeneratedOnAddOrUpdate() //Generate the value when the update is made and when data is added
            .HasDefaultValueSql("getutcdate()") //need to use HasDefaultValueSql with "getutcdate" because it need to be the sql command
            .HasColumnType("DATETIME2");

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