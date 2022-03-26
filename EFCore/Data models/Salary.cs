using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Salary
{
    public int Id { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal DiscretionaryBonus { get; set; }
    public decimal IncentiveBonus { get; set; }
    public decimal TaskBonus { get; set; }
    public Employee? Employee { get; set; }
    public List<SalaryTransfer>? SalaryTransfer { get; set; }
    public int? SalaryTransferId { get; set; }
}

public class SalaryEntityTypeConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("salary");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.BaseSalary)
            .HasDefaultValue(0)
            .HasPrecision(7, 2);

        builder.Property(s => s.DiscretionaryBonus)
            .HasDefaultValue(0)
            .HasPrecision(6, 2);

        builder.Property(s => s.IncentiveBonus)
            .HasDefaultValue(0)
            .HasPrecision(6, 2);

        builder.Property(s => s.TaskBonus)
            .HasDefaultValue(0)
            .HasPrecision(6, 2);

        builder.HasMany(s => s.SalaryTransfer)
            .WithOne(st => st.Salary)
            .HasForeignKey(s => s.SalaryId);
    }
}
