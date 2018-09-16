using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface IOrderService
    {
        //New Order
        Order New();

        //Create //POST
        Order CreateOrder(Order order);
        //Read //GET
        Order FindOrderById(int id);
        List<Order> GetAllOrders();
        //Update //PUT
        Order UpdateOrder(Order orderUpdate);
        
        //Delete //DELETE
        Order DeleteOrder(int id);
    }
}