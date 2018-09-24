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
            /*if (order.Customer != null &&
                _ctx.ChangeTracker.Entries<Customer>()
                .FirstOrDefault(ce => ce.Entity.Id == order.Customer.Id) == null)
            {
                _ctx.Attach(order.Customer);
            }
            var saved = _ctx.Orders.Add(order).Entity;
            _ctx.SaveChanges();
            return saved;*/

            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
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

        public Order Update(Order orderUpdate)
        {
            /*if (orderUpdate.Customer != null &&
                _ctx.ChangeTracker.Entries<Customer>()
                    .FirstOrDefault(ce => ce.Entity.Id == orderUpdate.Customer.Id) == null)
            {
                _ctx.Attach(orderUpdate.Customer);
            }
            else
            {
                _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            }
            var updated = _ctx.Update(orderUpdate).Entity;
            _ctx.SaveChanges();*/
            
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
            _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            _ctx.SaveChanges();

            return orderUpdate;
        }

        public Order Delete(int id)
        {
            var removed = _ctx.Remove(new Order {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}