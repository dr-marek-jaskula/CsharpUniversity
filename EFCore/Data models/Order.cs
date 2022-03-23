namespace EFCore.Data_models;

public class Order
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public DateOnly Deadline { get; set; }
    public Product Product { get; set; } = new();
    public int ProductId { get; set; }
    public Payment Payment { get; set; } = new();
    public int PaymentId { get; set; }
}

