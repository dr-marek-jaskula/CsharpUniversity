using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class WorkTask : WorkItem
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}

public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder.Property(wi => wi.EndDate)
            .HasPrecision(0);

        builder.Property(wi => wi.StartDate)
            .HasPrecision(0);

        builder.HasOne(wi => wi.Employee)
            .WithOne(e => e.CurrentTask)
            .HasForeignKey<WorkTask>(e => e.EmployeeId);
    }
}