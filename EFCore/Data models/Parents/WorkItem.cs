namespace EFCore.Data_models;

//Table-per-hierarchy approach (abstract class)

//The additional "Discriminator" column to distinguish the different children of a WorkItem
public abstract class WorkItem
{
    //Guid primary key
    public Guid Id { get; set; }
    public int Priority { get; set; }
    public Status Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}