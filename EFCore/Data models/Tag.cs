namespace EFCore.Data_models;

public class Tag
{
    public int Id { get; set; }
    public ProductTag ProductTag { get; set; }
    public virtual List<Product> Products { get; set; } = new();
}