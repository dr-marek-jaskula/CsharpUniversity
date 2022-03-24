﻿namespace EFCore.Data_models;

public class Order
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public Status Status { get; set; }
    public DateOnly Deadline { get; set; }
    public Product Product { get; set; } = new();
    public Payment Payment { get; set; } = new();
}
