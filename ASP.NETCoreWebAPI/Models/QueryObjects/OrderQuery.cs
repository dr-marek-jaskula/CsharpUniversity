using Microsoft.AspNetCore.Enums;

namespace ASP.NETCoreWebAPI.Models.QueryObjects;

public record class OrderQuery
(
    string SearchPhrase,
    int PageNumber,
    int PageSize,
    string SortBy,
    SortDirection SortDirection
);

//public class OrderQuery
//{
//    public string SearchPhrase { get; set; } = string.Empty;
//    public int PageNumber { get; set; }
//    public int PageSize { get; set; }
//    public string SortBy { get; set; } = string.Empty;
//    public SortDirection SortDirection { get; set; }
//}