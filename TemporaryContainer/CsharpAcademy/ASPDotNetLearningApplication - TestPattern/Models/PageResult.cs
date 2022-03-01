using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
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
            //element  jeden po pominiêtych, czyli pierwszy element
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            //ostatni element przed pominiêciem
            ItemsTo = ItemsFrom + pageSize - 1;

            //ilosc stron, ukróci u³amek wiec jedn¹ wiêcej (mo¿na by po prostu +1)
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
        }
    }
}
