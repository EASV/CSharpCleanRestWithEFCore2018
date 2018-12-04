using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface ICustomerRepository
    {
        //CustomerRepository Interface
        //Create Data
        //No Id when enter, but Id when exits
        Customer Create(Customer customer);
        //Read Data
        Customer ReadyById(int id);
        List<CustomerType> ReadCustomerTypes();
        FilteredList<Customer> ReadAll(Filter filter);
        int Count();
        //Update Data
        Customer Update(Customer customerUpdate);
        //Delete Data
        Customer Delete(int id);
        Customer ReadyByIdIncludeOrders(int id);
    }
}
