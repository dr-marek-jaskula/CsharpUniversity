namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class CustomerDto
(
    int Id,
    string FirstName,
    string LastName,
    string Gender,
    string DateOfBirth,
    string ContactNumber,
    string Email,
    string Rank
);