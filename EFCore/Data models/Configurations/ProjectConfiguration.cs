﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models.Configurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasOne(wi => wi.ProjectLeader)
            .WithOne(e => e.Project)
            .HasForeignKey<Project>(e => e.ProjectLeaderId);
    }
}