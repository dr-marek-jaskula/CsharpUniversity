namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class RegisterUserDto
(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    int RoleId = 1,
    int? EmployeeId = null,
    int? CustomerId = null
);