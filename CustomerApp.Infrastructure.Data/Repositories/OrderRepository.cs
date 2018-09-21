using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        readonly CustomerAppContext _ctx;

        public OrderRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }
        
        public Order Create(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Order ReadyById(int id)
        {
            return _ctx.Orders.Include(o => o.Customer)
                .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> ReadAll()
        {
            return _ctx.Orders;
        }

        public Order Update(Order OrderUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Order Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}