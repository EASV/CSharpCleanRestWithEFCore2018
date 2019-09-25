using System;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CustomerAppContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            var type1 = ctx.CustomerTypes.Add(new CustomerType()
            {
                Name = "Great White Shark"
            }).Entity;
            var type2 = ctx.CustomerTypes.Add(new CustomerType()
            {
                Name = "Old Yellow"
            }).Entity;
            var cust1 = ctx.Customers.Add(new Customer()
            {
                Address = "BongiStreet",
                FirstName = "John",
                LastName = "Olesen",
                Type = type1
            }).Entity;
                    
            var cust2 = ctx.Customers.Add(new Customer()
            {
                Address = "BongiStreet 22",
                FirstName = "Bill",
                LastName = "Bøllesen"
            }).Entity;
            for (var i = 0; i < 1000; i++)
            {
                ctx.Customers.Add(new Customer()
                {
                    Address = $"Street {i}",
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    Type = i%2==0 ? type1: type2
                });
            }
                    
            var order1 = ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            }).Entity;
            ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });
            ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust2
            });
            var prod = ctx.Products.Add(new Product()
            {
                Name = "smølf"
            }).Entity;
            ctx.Products.Add(new Product()
            {
                Name = "Ko"
            });
            ctx.OrderLines.Add(new OrderLine()
            {
                Product = prod,
                Order = order1
            });

            var role1 = ctx.Roles.Add( new Role { Name = "Guest" }).Entity;
            ctx.Roles.Add( new Role { Name = "User" });
            var role3 = ctx.Roles.Add( new Role { Name = "Administrator" }).Entity;
            var role4 = ctx.Roles.Add( new Role { Name = "SuperAdministrator" }).Entity;

            ctx.Users.Add(
                new User()
                {
                    UserName = "blinko",
                    Email = "blinko@inko.dk",
                    PasswordHash = "AQAAAAEAACcQAAAAEFE8XWu6lIyinwsA4bBYJiOvabmOqZoURROPGY/eJdiNES+RGLLU7VW+/g3I+aFepA==",
                    Role = role1
                }
            );
            ctx.Users.Add(
                new User()
                {
                    UserName = "dinko",
                    Email = "dinko@inko.dk",
                    PasswordHash = "AQAAAAEAACcQAAAAENLKdwf9yrsIwY92GvwzYNVkXgdjoqWkgtt2TNlExnM+8lHORdurnPFszwiVYvJrwQ==",
                    Role = role3
                }
            );
            ctx.SaveChanges();
        }
    }
}