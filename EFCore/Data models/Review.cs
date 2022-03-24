namespace EFCore.Data_models;

public class Review
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Stars { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Product? Product { get; set; }
    public Employee? Employee { get; set; }
}

