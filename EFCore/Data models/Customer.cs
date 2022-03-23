namespace EFCore.Data_models;

public class Customer : Person
{
    public int Id { get; set; }
    public Rank Rank { get; set; }
}

