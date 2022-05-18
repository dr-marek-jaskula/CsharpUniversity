using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EFCore.Data_models;

public class Task : WorkItem
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}

public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<Data_models.Task>
{
    public void Configure(EntityTypeBuilder<Data_models.Task> builder)
    {
        builder.Property(wi => wi.EndDate)
            .HasPrecision(0);

        builder.Property(wi => wi.StartDate)
            .HasPrecision(0);

        builder.HasOne(wi => wi.Employee)
            .WithOne(e => e.CurrentTask)
            .HasForeignKey<Data_models.Task>(e => e.EmployeeId);
    }
}