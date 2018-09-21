using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}