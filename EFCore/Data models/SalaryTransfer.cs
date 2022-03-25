using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class SalaryTransfer
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public bool IsDiscretionaryBonus { get; set; }
    public bool IsIncentiveBonus { get; set; }
    public bool IsTaskBonus { get; set; }
    public Salary Salary { get; set; } = new();
    public Employee Employee { get; set; } = new();
}

public class SalaryTransferEntityTypeConfiguration : IEntityTypeConfiguration<SalaryTransfer>
{
    public void Configure(EntityTypeBuilder<SalaryTransfer> builder)
    {
        builder.ToTable("salary_transfer");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.Date)
            .IsRequired(true)
            .HasColumnType("DATE");

        builder.Property(s => s.IsTaskBonus)
            .HasDefaultValue(false)
            .HasColumnType("BIT");

        builder.Property(s => s.IsDiscretionaryBonus)
            .HasDefaultValue(false)
            .HasColumnType("BIT");

        builder.Property(s => s.IsIncentiveBonus)
            .HasDefaultValue(false)
            .HasColumnType("BIT");
    }
}

