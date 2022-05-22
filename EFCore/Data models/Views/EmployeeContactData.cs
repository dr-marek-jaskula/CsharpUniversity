namespace EFCore.Data_models.Views;

//The model needs to fit the view (names should be the same or it should be designed using configurations)
public class EmployeeContactData
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Manager { get; set; } = string.Empty;
}