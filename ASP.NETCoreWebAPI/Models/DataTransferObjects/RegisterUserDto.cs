namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class RegisterUserDto(string Email, string Password, string ConfirmPassword, string? Nationality, DateTime? DateOfBirth, int RoleId = 1);