using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public int? PersonId { get; set; }
    public virtual Person? Person { get; set; }
}

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).UseIdentityColumn();

        builder.Property(u => u.Username)
            .HasColumnType("VARCHAR(60)")
            .IsRequired(true);

        builder.Property(u => u.Email)
            .IsRequired(true)
            .HasColumnType("VARCHAR(40)");

        builder.Property(u => u.CreatedDate)
            .HasDefaultValueSql("getutcdate()") //need to use HasDefaultValueSql with "getutcdate" because it need to be the sql command
            .HasColumnType("DATE");

        builder.Property(u => u.PasswordHash)
            .HasColumnType("NCHAR(514)"); //512 + 2 for 'N' characters

        builder.Property(u => u.RoleId)
            .HasColumnType("TINYINT");

        builder.HasOne(u => u.Person)
            .WithOne(c => c.User)
            .HasForeignKey<User>(u => u.PersonId);
    }
}