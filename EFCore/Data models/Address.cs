namespace EFCore.Data_models;

public class Address
{
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int PostalCode { get; set; }
    public int Street { get; set; }
    public int Building { get; set; }
    public int? Flat { get; set; }
}


