namespace EFCore.Data_models;

public class ProductAmount
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public Product Product { get; set; } = new();
    public Shop Shop { get; set; } = new();
}

