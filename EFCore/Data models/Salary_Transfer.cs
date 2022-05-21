namespace EFCore.Data_models;

public class Salary_Transfer
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsDiscretionaryBonus { get; set; }
    public bool IsIncentiveBonus { get; set; }
    public bool IsTaskBonus { get; set; }
    public virtual Salary? Salary { get; set; }
    public int? SalaryId { get; set; }
}