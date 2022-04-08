using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Salary
{
    public int Id { get; set; }
    public int BaseSalary { get; set; }
    public int DiscretionaryBonus { get; set; }
    public int IncentiveBonus { get; set; }
    public int TaskBonus { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual List<Salary_Transfer> SalaryTransfer { get; set; } = new();
}

public class SalaryEntityTypeConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("Salary");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.BaseSalary)
            .HasDefaultValue(0)
            .HasColumnType("INT");

        builder.Property(s => s.DiscretionaryBonus)
            .HasDefaultValue(0)
            .HasColumnType("INT");

        builder.Property(s => s.IncentiveBonus)
            .HasDefaultValue(0)
            .HasColumnType("INT");

        builder.Property(s => s.TaskBonus)
            .HasDefaultValue(0)
            .HasColumnType("INT");

        builder.HasMany(s => s.SalaryTransfer)
            .WithOne(st => st.Salary)
            .HasForeignKey(s => s.SalaryId);
    }
}
