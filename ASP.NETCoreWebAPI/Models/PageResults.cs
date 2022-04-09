namespace ASP.NETCoreWebAPI.Models;

public class PageResult<T>
{
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public int TotalItemsCount { get; set; }

    public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;

        //FirstElement
        ItemsFrom = pageSize * (pageNumber - 1) + 1;

        //LastElement
        ItemsTo = ItemsFrom + pageSize - 1;

        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
