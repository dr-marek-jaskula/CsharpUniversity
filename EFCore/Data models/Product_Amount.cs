namespace EFCore.Data_models;

public class Product_Amount
{
    public int Amount { get; set; }
    public virtual Product? Product { get; set; }
    public int? ProductId { get; set; }
    public virtual Shop? Shop { get; set; }
    public int? ShopId { get; set; }
}