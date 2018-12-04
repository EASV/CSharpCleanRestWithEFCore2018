using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class FilteredList <T>
    {
        public IEnumerable<T> List { get; set; }
        public int Count { get; set; }
    }
}