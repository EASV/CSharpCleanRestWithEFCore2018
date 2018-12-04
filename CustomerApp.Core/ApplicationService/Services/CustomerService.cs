using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class CustomerService: ICustomerService
    {
        readonly ICustomerRepository _customerRepo;
        readonly IOrderRepository _orderRepo;

        public CustomerService(ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            _customerRepo = customerRepository;
            _orderRepo = orderRepository;
        }

        public Customer NewCustomer(string firstName, string lastName, string address)
        {
            var cust = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };

            return cust;
        }

        public Customer CreateCustomer(Customer cust)
        {
            return _customerRepo.Create(cust);
        }

        public Customer FindCustomerById(int id)
        {
            return _customerRepo.ReadyById(id);
        }

        public Customer FindCustomerByIdIncludeOrders(int id)
        {
            return _customerRepo.ReadyByIdIncludeOrders(id);
        }

        public FilteredList<Customer> GetAllCustomers(Filter filter = null)
        {
            return _customerRepo.ReadAll(filter);
        }

        public List<CustomerType> ReadCustomerTypes()
        {
            return _customerRepo.ReadCustomerTypes();
        }


        public Customer UpdateCustomer(Customer customerUpdate)
        {
            return _customerRepo.Update(customerUpdate);
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepo.Delete(id);
        }

        public int Count()
        {
            return _customerRepo.Count();
        }
    }
}
