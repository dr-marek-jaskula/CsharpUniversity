namespace EFCore.Data_models;

public class Project : WorkItem
{
    public virtual Employee? ProjectLeader { get; set; }
    public virtual int ProjectLeaderId { get; set; }
}