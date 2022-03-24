namespace EFCore.Data_models;

public class Payment
{
    public int Id { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateOnly Deadline { get; set; }
    public Status Status { get; set; }
    public Product Product { get; set; } = new();
}

