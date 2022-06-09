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

        //Total number of pages
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}

//Other version of pagination:
/*
public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
*/