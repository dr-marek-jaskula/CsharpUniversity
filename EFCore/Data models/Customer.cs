namespace EFCore.Data_models;

public class Customer : Person
{
    public Rank Rank { get; set; }
    public virtual List<Order> Orders { get; set; } = new();
}