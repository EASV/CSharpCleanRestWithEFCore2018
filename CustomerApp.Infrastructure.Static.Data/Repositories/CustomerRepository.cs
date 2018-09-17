using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        public CustomerRepository()
        {
            if (FakeDB.Customers.Count >= 1) return;
            var cust1 = new Customer()
            {
                Id = FakeDB.Id ++,
                FirstName = "Bob",
                LastName = "Dylan",
                Address = "BongoStreet 202"
            };
            FakeDB.Customers.Add(cust1);

            var cust2 = new Customer()
            {
                Id = FakeDB.Id ++,
                FirstName = "Lars",
                LastName = "Bilde",
                Address = "Ostestrasse 202"
            };
            FakeDB.Customers.Add(cust2);
        }
        
        public Customer Create(Customer customer)
        {
            customer.Id = FakeDB.Id++;
            FakeDB.Customers.Add(customer);
            return customer;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return FakeDB.Customers;
        }

        public Customer ReadyById(int id)
        {
            return FakeDB.Customers.
                Select(c => new Customer()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    Address = c.Address,
                    LastName = c.LastName
                }).
                FirstOrDefault(c => c.Id == id);
            
        }

        //Remove later when we use UOW
        public Customer Update(Customer customerUpdate)
        {
            var customerFromDB = ReadyById(customerUpdate.Id);
            if (customerFromDB == null) return null;
            
            customerFromDB.FirstName = customerUpdate.FirstName;
            customerFromDB.LastName = customerUpdate.LastName;
            customerFromDB.Address = customerUpdate.Address;
            return customerFromDB;
        }

        public Customer Delete(int id)
        {
            var customerFound = ReadyById(id);
            if (customerFound == null) return null;
            
            FakeDB.Customers.Remove(customerFound);
            return customerFound;
        }

        public Customer ReadyByIdIncludeOrders(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
