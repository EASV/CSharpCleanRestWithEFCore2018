using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            order = _ctx.Add(order).Entity;
            if (order.OrderLines != null)
            {
                foreach (var ol in order.OrderLines)
                {
                    ol.OrderId = order.Id;
                    ol.ProductId = ol.ProductId;
                    _ctx.Add(ol);
                }
            } */

            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order ReadyById(int id)
        {
            return _ctx.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        public FilteredList<Order> ReadAll(Filter filter)
        {
            if (filter == null)
            {
                return new FilteredList<Order>() {
                    List = _ctx.Orders
                            .AsNoTracking()
                            .ToList(), 
                    Count = _ctx.Orders.Count()
                };
            }

            var items = _ctx.Orders
                .AsNoTracking()
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .Include(o => o.Customer)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .ToList();
            return new FilteredList<Order>() {List = items, Count = Count()};
            
        }

        public int Count()
        {
            return _ctx.Orders.Count();
        }

        public Order Update(Order orderUpdate)
        {
            //Clone orderlines to new location in memory, so they are not overridden on Attach
            var newOrderLines = new List<OrderLine>(orderUpdate.OrderLines);
            //Attach order so basic properties are updated
            _ctx.Entry(orderUpdate).State = EntityState.Modified;
            //Remove all orderlines with updated order information
            _ctx.OrderLines.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.OrderId == orderUpdate.Id)
            );
            //Add all orderlines with updated order information
            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }
            //Update customer relation
            _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            // Save it
            _ctx.SaveChanges();
            //Return it
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