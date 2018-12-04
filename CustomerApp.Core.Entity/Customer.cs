using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public CustomerType Type { get; set; }
        
        public List<Order> Orders { get; set; }
    }
}
