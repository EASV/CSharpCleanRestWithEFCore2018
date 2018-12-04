using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface ICustomerService
    {
        //New Customer
        Customer NewCustomer(string firstName,
                                string lastName,
                                string address);

        //Create //POST
        Customer CreateCustomer(Customer cust);
        //Read //GET
        Customer FindCustomerById(int id);
        Customer FindCustomerByIdIncludeOrders(int id);
        FilteredList<Customer> GetAllCustomers(Filter filter);
        List<CustomerType> ReadCustomerTypes();
        int Count();
        //Update //PUT
        Customer UpdateCustomer(Customer customerUpdate);
        
        //Delete //DELETE
        Customer DeleteCustomer(int id);
    }
}
