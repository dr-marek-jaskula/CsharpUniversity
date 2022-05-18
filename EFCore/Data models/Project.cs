using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class Project : WorkItem
{
    public virtual Employee? ProjectLeader { get; set; }
    public virtual int ProjectLeaderId { get; set; }
}

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasOne(wi => wi.ProjectLeader)
            .WithOne(e => e.Project)
            .HasForeignKey<Project>(e => e.ProjectLeaderId);
    }
}