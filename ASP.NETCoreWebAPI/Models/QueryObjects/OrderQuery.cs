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
