namespace EFCore.Data_models;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    List<Employee> Employees { get; set; } = new();
    List<Order> Orders { get; set; } = new();
    List<Product> Products { get; set; } = new();
}

