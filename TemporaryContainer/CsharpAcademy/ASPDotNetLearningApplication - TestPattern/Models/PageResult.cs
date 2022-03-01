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
            //element  jeden po pomini�tych, czyli pierwszy element
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            //ostatni element przed pomini�ciem
            ItemsTo = ItemsFrom + pageSize - 1;

            //ilosc stron, ukr�ci u�amek wiec jedn� wi�cej (mo�na by po prostu +1)
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
        }
    }
}
