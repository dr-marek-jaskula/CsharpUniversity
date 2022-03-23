namespace EFCore.Data_models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Store? Store { get; set; }
    public Shop? Shop { get; set; }
}

