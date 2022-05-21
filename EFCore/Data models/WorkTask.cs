namespace EFCore.Data_models;

public class WorkTask : WorkItem
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}