namespace EFCore.Data_models;

public class Salary
{
    public int Id { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal DiscretionaryBonus { get; set; }
    public decimal IncentiveBonus { get; set; }
    public decimal TaskBonus { get; set; }
    public Employee? Employee { get; set; }
}