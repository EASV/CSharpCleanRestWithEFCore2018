using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class OrderService: IOrderService
    {
        readonly IOrderRepository _orderRepo;
        readonly ICustomerRepository _customerRepo;

        public OrderService(IOrderRepository orderRepo,
            ICustomerRepository customerRepository)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepository;
        }
        
        public Order New()
        {
            return new Order();
        }

        public Order CreateOrder(Order order)
        {
            if(order.Customer == null || order.Customer.Id <= 0)
                throw new InvalidDataException("To create Order you need a Customer");
            if(_customerRepo.ReadyById(order.Customer.Id) == null)
                throw new InvalidDataException("Customer Not found");
            if(order.OrderDate == null)
                throw new InvalidDataException("Order needs a Order Date");

            return _orderRepo.Create(order);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepo.ReadyById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.ReadAll().ToList();
        }

        public List<Order> GetFilteredOrders(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPage Must zero or more");
            }
            if((filter.CurrentPage -1 * filter.ItemsPrPage) >= _orderRepo.Count())
            {
                throw new InvalidDataException("Index out bounds, CurrentPage is to high");
            }

            return _orderRepo.ReadAll(filter).ToList();
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            return _orderRepo.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepo.Delete(id);
        }
    }
}