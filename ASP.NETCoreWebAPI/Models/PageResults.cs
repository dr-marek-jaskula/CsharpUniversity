namespace ASP.NETCoreWebAPI.Models;

//Generic pagination schema

//Generic class for paginating different objects
public class PageResult<T>
{
    //Generic list that stores the pagination result
    public List<T> Items { get; set; }

    //Total amount of pages that store the results
    public int TotalPages { get; set; }

    //The first element of the certain page
    public int ItemsFrom { get; set; }

    //The last element of the certain page
    public int ItemsTo { get; set; }

    //Total number or items that are stored in pages
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