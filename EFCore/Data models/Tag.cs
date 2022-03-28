using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Tag
{
    public int Id { get; set; }
    public ProductTag ProductTag { get; set; }
    public virtual List<Product_Tag> Product_Tags { get; set; } = new();
}

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tag");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseIdentityColumn();

        builder.Property(p => p.ProductTag)
            .IsRequired(true)
            .HasColumnType("CHAR(7)")
            .HasConversion(pt => pt.ToString(),
            s => (ProductTag)Enum.Parse(typeof(ProductTag), s));
    }
}

