namespace EFCore.Data_models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public virtual List<Tag> Tags { get; set; } = new();
    public virtual List<Review> Reviews { get; set; } = new();
    public virtual List<Shop> Shops { get; set; } = new();
    public virtual List<Order> Order { get; set; } = new();
}