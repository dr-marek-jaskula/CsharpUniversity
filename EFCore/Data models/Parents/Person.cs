namespace EFCore.Data_models;

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public int? AddressId { get; set; }
}

