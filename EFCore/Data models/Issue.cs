using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class Issue : WorkItem
{
    public decimal Efford { get; set; }
}

public class IssueEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(wi => wi.Efford)
            .HasColumnType("decimal(5,2)");
    }
}