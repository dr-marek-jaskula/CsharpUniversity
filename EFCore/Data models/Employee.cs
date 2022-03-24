namespace EFCore.Data_models;

public class Employee : Person
{
    public int Id { get; set; }
    public DateTime HireDate { get; set; }
    public Salary Salary { get; set; } = new();
}