﻿namespace EFCore.EF_Core_basics;

public class QueryWithInclude
{
    //The following query
    //var order = _dbContex.Orders
    //    .Include(o => o.Product)
    //        .ThenInclude(p => p == null ? null : p.Shops)
    //    .Include(o => o.Product) //To get the Tags also
    //        .ThenInclude(p => p == null ? null : p.Tags)
    //    .Include(o => o.Payment)
    //    .FirstOrDefault(o => o.Id == id);
}