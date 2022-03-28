﻿using EFCore.Data_models;

namespace EFCore;

public static partial class Helpers
{
    public static decimal CalculateTotal(Order order)
    {
        if (order.Payment.Discount is not null)
            return Math.Round(order.Product.Price * order.Amount * (1 - (decimal)order.Payment.Discount), 2);
        else
            return Math.Round(order.Product.Price * order.Amount, 2);
    }
}


