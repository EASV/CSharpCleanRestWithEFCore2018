using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Moq;
using Xunit;

namespace TestCore.ApplicationService.Service
{
    public class CustomerServiceTest
    {
        [Fact]
        public void CreateNullCustomerThrowsException()
        {
            var custRepo = new Mock<ICustomerRepository>();
            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateCustomer(null));
            Assert.Equal("Customer Needs to be there", ex.Message);  

        }
        
        [Fact]
        public void CreateCustomerWithMissingFirstNameThrowsException()
        {
            var custRepo = new Mock<ICustomerRepository>();
            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            var cust = new Customer() { };
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateCustomer(cust));
            Assert.Equal("Customer Needs a FirstName", ex.Message);  

        }
        
        [Fact]
        public void CreateCustomerWithBlankFirstNameThrowsException()
        {
            var custRepo = new Mock<ICustomerRepository>();
            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            var cust = new Customer() {FirstName = ""};
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateCustomer(cust));
            Assert.Equal("Customer FirstName Cant Be Blank", ex.Message);  

        }
        
        [Fact]
        public void CreateCustomerWithTypeTheTypeShouldExistThrowsException()
        {
            var Type = new CustomerType()
            {
                Id = 1,
                Name = "TheType"
            };
            
            var custRepo = new Mock<ICustomerRepository>();
            /*custRepo.Setup(x => x.ReadCustomerTypeById(It.IsAny<int>()))
                .Returns(null);*/

            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            
            var cust = new Customer()
            {
                FirstName = "John",
                Type = Type
            };
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateCustomer(cust));
//orderRepo.Verify(x => x.Create(It.IsAny<Order>()), Times.Once);

        }
        
        [Fact]
        public void CreateCustomerWithTypeShouldOnlyCallReadTypeIdOnce()
        {
            var Type = new CustomerType()
            {
                Id = 1,
                Name = "TheType"
            };
            
            var custRepo = new Mock<ICustomerRepository>();
            custRepo.Setup(x => x.ReadCustomerTypeById(It.IsAny<int>()))
                .Returns(Type);

            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            
            var cust = new Customer()
            {
                FirstName = "John",
                Type = Type
            };
            service.CreateCustomer(cust);
            custRepo.Verify(x => x.ReadCustomerTypeById(Type.Id), Times.Once);

        }
        
        
        
        [Fact]
        public void SearchReturnsCorrectCustomers()
        {
            var customers = new List<Customer>();
            for (int i = 0; i < 1000; i++)
            {
                customers.Add(new Customer()
                {
                    Id = i,
                    FirstName = "Do" + i
                });
            }
            var filter = new Filter()
            {
                CurrentPage = 1,
                ItemsPrPage = 10,
                SearchTextFirstName = "Do1"
            };
            var expectedFilteredList = new FilteredList<Customer>()
            {
                List = customers.Where(c => c.FirstName.Contains(filter.SearchTextFirstName)),
                Count = customers.Count
            };
            var custRepo = new Mock<ICustomerRepository>();
            custRepo.Setup(cr => cr.ReadAll(filter))
                .Returns(expectedFilteredList);
            var orderRepo = new Mock<IOrderRepository>();

            var service = new CustomerService(custRepo.Object, orderRepo.Object);

            var foundCustomers = service.GetAllCustomers(filter);
            Assert.Equal(expectedFilteredList, foundCustomers);
        }
    }
}