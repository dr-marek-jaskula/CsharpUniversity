namespace EFCore.Data_models;

public class Product_Tag
{
    public int Id { get; set; }
    public Product Product { get; set; } = new();
    public int ProductId { get; set; }
    public Tag Tag { get; set; } = new();
    public int TagId { get; set; }
}

