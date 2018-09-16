using System;

namespace CustomerApp.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Customer Customer { get; set; }
    }
}