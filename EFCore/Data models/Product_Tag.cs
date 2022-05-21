namespace EFCore.Data_models;

public class Product_Tag
{
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Tag? Tag { get; set; }
    public int? TagId { get; set; }
}