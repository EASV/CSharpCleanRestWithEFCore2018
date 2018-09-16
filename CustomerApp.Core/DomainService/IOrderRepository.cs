using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface IOrderRepository
    {
        //Create Data
        //No Id when enter, but Id when exits
        Order Create(Order order);
        //Read Data
        Order ReadyById(int id);
        IEnumerable<Order> ReadAll();
        //Update Data
        Order Update(Order OrderUpdate);
        //Delete Data
        Order Delete(int id);
    }
}