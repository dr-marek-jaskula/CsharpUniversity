using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data_models;

public class Product_Tag
{
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Tag? Tag { get; set; }
    public int? TagId { get; set; }
}

public class Product_TagEntityTypeConfiguration : IEntityTypeConfiguration<Product_Tag>
{
    public void Configure(EntityTypeBuilder<Product_Tag> builder)
    {
        builder.ToTable("Product_Tag");

        builder.HasKey(pt => new { pt.ProductId, pt.TagId });
    }
}