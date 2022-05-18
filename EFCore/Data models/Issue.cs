using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class Issue : WorkItem 
{
    public decimal Cost { get; set; }
}

public class IssueEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(wi => wi.Cost)
            .HasColumnType("decimal(6,2)");
    }
}