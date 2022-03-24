namespace EFCore.Data_models;

public class SalaryTransfer
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public bool IsDiscretionaryBonus { get; set; }
    public bool IsIncentiveBonus { get; set; }
    public bool IsTaskBonus { get; set; }
    public Salary Salary { get; set; } = new();
    public Employee Employee { get; set; } = new();
}
