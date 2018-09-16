using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Static.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
            if (FakeDB.Orders.Count > 0) return;
            
            var order1 = new Order()
            {
                Id = FakeDB.OrderId++,
                DeliveryDate = DateTime.Now.AddMonths(2),
                OrderDate = DateTime.Now.AddMonths(-1),
                Customer = new Customer(){Id = 1}
            };
            FakeDB.Orders.Add(order1);
            
            var order2 = new Order()
            {
                Id = FakeDB.OrderId++,
                DeliveryDate = DateTime.Now.AddMonths(1),
                OrderDate = DateTime.Now.AddDays(-2),
                Customer = new Customer(){Id = 1}
            };
            FakeDB.Orders.Add(order2);
            
        }
        
        public Order Create(Order order)
        {
            order.Id = FakeDB.OrderId++;
            FakeDB.Orders.Add(order);
            return order;
        }

        public Order ReadyById(int id)
        {
            return FakeDB.Orders.FirstOrDefault(order => order.Id == id);
        }

        public IEnumerable<Order> ReadAll()
        {
            return FakeDB.Orders;
        }

        public Order Update(Order orderUpdate)
        {
            var orderFromDB = ReadyById(orderUpdate.Id);
            if (orderFromDB == null) return null;
            
            orderFromDB.DeliveryDate = orderUpdate.DeliveryDate;
            orderFromDB.OrderDate = orderUpdate.OrderDate;
            if (orderUpdate.Customer != null && orderFromDB.Customer != null)
            {
                orderFromDB.Customer.Id = orderUpdate.Customer.Id;
            }
            return orderFromDB;
        }

        public Order Delete(int id)
        {
            var orderFound = ReadyById(id);
            if (orderFound == null) return null;
            
            FakeDB.Orders.Remove(orderFound);
            return orderFound;
        }
    }
}