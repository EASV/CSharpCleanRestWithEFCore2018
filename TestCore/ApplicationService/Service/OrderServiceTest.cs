using System;
using System.Collections.Generic;
using System.IO;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Moq;
using Xunit;

namespace TestCore.ApplicationService.Service
{
    public class OrderServiceTest
    {
        public OrderServiceTest()
        {
            //Add reusable stuff
        }

        public void Dispose()
        {
            //Dispose Stuff we dont need anymore
        }
        
        [Fact]
        public void CreateOrderWithCustomerMissingThrowsException()
        {
            var custRepo = new Mock<ICustomerRepository>();
            var orderRepo = new Mock<IOrderRepository>();
            IOrderService service = 
                new OrderService(orderRepo.Object, custRepo.Object);
            var order = new Order()
            {
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now
            };
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateOrder(order));
            Assert.Equal("To create Order you need a Customer", ex.Message);  
        }
        
        [Fact]
        public void CreateOrderDeliveryDateMissingThrowsException()
        {
            var custRepo = new Mock<ICustomerRepository>();
            custRepo.Setup(x => x.ReadyById(It.IsAny<int>()))
                .Returns(new Customer(){Id = 1});
            var orderRepo = new Mock<IOrderRepository>();
            IOrderService service = 
                new OrderService(orderRepo.Object, custRepo.Object);
            var order = new Order()
            {
                Customer = new Customer() {Id = 1},
                OrderDate = DateTime.Now
            };
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateOrder(order));
            Assert.Equal("To create Order you need a deliveryDate", ex.Message);  
        }
        
        [Fact]
        public void CreateOrderShouldCallOrderRepoCreateOrderOnce()
        {
            var custRepo = new Mock<ICustomerRepository>();
            custRepo.Setup(x => x.ReadyById(It.IsAny<int>()))
                .Returns(new Customer(){Id = 1});

            var orderRepo = new Mock<IOrderRepository>();
            IOrderService service = 
                new OrderService(orderRepo.Object, custRepo.Object);
            var order = new Order()
            {
                Customer = new Customer(){Id = 1},
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now
            };
            service.CreateOrder(order);
            orderRepo.Verify(x => x.Create(It.IsAny<Order>()), Times.Once);
            custRepo.Verify(x => x.ReadyById(It.IsAny<int>()), Times.Once);

        }

    }
}