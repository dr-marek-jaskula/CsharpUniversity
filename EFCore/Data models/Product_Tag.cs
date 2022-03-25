﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Product_Tag
{
    public int Id { get; set; }
    public Product Product { get; set; } = new();
    public int ProductId { get; set; }
    public Tag Tag { get; set; } = new();
    public int TagId { get; set; }
}

public class Product_TagEntityTypeConfiguration : IEntityTypeConfiguration<Product_Tag>
{
    public void Configure(EntityTypeBuilder<Product_Tag> builder)
    {
        builder.ToTable("product_tag");

        builder.HasKey(pt => pt.Id);
        builder.Property(pt => pt.Id).UseIdentityColumn();

        builder.HasOne(pt => pt.Product)
           .WithMany(p => p.Product_Tags)
           .HasForeignKey(pt => pt.ProductId);

        builder.HasOne(pt => pt.Tag)
            .WithMany(t => t.Product_Tags)
            .HasForeignKey(pt => pt.TagId);
    }
}
