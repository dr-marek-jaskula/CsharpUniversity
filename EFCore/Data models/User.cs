namespace EFCore.Data_models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly CreatedDate { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public int? PersonId { get; set; }
    public virtual Person? Person { get; set; }
}