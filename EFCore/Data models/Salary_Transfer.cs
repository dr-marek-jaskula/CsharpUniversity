using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Salary_Transfer
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsDiscretionaryBonus { get; set; }
    public bool IsIncentiveBonus { get; set; }
    public bool IsTaskBonus { get; set; }
    public virtual Salary? Salary { get; set; }
    public int? SalaryId { get; set; }
}

public class SalaryTransferEntityTypeConfiguration : IEntityTypeConfiguration<Salary_Transfer>
{
    public void Configure(EntityTypeBuilder<Salary_Transfer> builder)
    {
        builder.ToTable("Salary_Transfer");

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

