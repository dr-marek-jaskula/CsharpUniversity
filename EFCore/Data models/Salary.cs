namespace EFCore.Data_models;

public class Salary
{
    public int Id { get; set; }
    public int BaseSalary { get; set; }
    public int DiscretionaryBonus { get; set; }
    public int IncentiveBonus { get; set; }
    public int TaskBonus { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual List<Salary_Transfer> SalaryTransfer { get; set; } = new();
}