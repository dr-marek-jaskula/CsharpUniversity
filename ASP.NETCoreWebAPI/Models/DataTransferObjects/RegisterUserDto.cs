namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class RegisterUserDto
(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    int RoleId = 1,
    int? PersonId = null
);