namespace ASP.NETCoreWebAPI.DatabaseEntitiesWebApi;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public DateTime? CreateTime { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
}
