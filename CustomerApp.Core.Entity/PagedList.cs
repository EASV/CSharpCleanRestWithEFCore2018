using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class PagedList<T>
    {
        public int Count { get; set; }
        public List<T> Items { get; set; }
    }
}