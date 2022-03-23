namespace EFCore.Data_models;

public class Transaction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; } 
    public Status Status { get; set; }
    public Order Order { get; set; } = new();
    public int OrderId { get; set; }
    public Customer Customer { get; set; } = new();
    public int CustomerId { get; set; }
}

