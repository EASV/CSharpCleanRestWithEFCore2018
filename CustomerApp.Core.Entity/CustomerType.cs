using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Customer> Customers { get; set; }
    }
}