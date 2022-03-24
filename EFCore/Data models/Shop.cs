namespace EFCore.Data_models;

public class Shop
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}

