namespace EFCore.Data_models;

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int PhoneNumber { get; set; }
    public int Email { get; set; }
    public Address? Address { get; set; }
}

