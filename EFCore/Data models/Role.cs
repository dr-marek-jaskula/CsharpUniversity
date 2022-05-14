using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual List<User> Users { get; set; } = new();
}

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnType("TINYINT").UseIdentityColumn();

        builder.Property(r => r.Name)
            .HasDefaultValue("Customer")
            .HasColumnType("VARCHAR(13)")
            .HasComment("Customer, Employee, Manager, Administrator");

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        //Inserting static data (data that are not related to other)
        builder.HasData(
            new Role() { Id = 1, Name = "Customer" },
            new Role() { Id = 2, Name = "Employee" },
            new Role() { Id = 3, Name = "Manager" },
            new Role() { Id = 4, Name = "Administrator" }
            );
    }
}