namespace EFCore.Data_models;

//Table-per-type approach
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public int? AddressId { get; set; }

    //One to one relationship with User table (User, UserId)
    public virtual User? User { get; set; }
}