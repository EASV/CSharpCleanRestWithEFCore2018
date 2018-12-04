using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data
{
    public class DBInitializerProd
    {
        public static void SeedDB(CustomerAppContext ctx)
        {    
            ctx.Database.EnsureCreated();

            ctx.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS " +
                                           "dbo.OrderLines, dbo.Orders");
            ctx.SaveChanges();
            
            var customerTypes = new List<CustomerType>()
            {
                new CustomerType(){ Name = "Guest" },
                new CustomerType(){ Name = "VIP" },
                new CustomerType(){ Name = "Rich" },
                new CustomerType(){ Name = "Soo Poor" },
                new CustomerType(){ Name = "Fun" }
            };
            ctx.AddRange(customerTypes);
            ctx.SaveChanges();
            
            var customers = new List<Customer>()
            {
                new Customer(){ FirstName = "Bill1", LastName = "Billson1", Address= "StreetRoad 1122", Type = new CustomerType(){Id = 1}},
                new Customer(){ FirstName = "Bill2", LastName = "Billson2", Address= "StreetRoad 2122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill3", LastName = "Billson3", Address= "StreetRoad 3122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill4", LastName = "Billson4", Address= "StreetRoad 4122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill5", LastName = "Billson5", Address= "StreetRoad 5122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill6", LastName = "Billson6", Address= "StreetRoad 6122", Type = new CustomerType(){Id = 4}},
                new Customer(){ FirstName = "Bill7", LastName = "Billson7", Address= "StreetRoad 7122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill8", LastName = "Billson8", Address= "StreetRoad 8122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill9", LastName = "Billson9", Address= "StreetRoad 9122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill10", LastName = "Billson10", Address= "StreetRoad 10122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill11", LastName = "Billson11", Address= "StreetRoad 11122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill12", LastName = "Billson12", Address= "StreetRoad 12122", Type = new CustomerType(){Id = 4}},
                new Customer(){ FirstName = "Bill13", LastName = "Billson13", Address= "StreetRoad 13122", Type = new CustomerType(){Id = 4}},
                
            };
            
            ctx.AddRange(customers);
            
            var role1 = ctx.Roles.Add( new Role { Name = "Guest" }).Entity;
            ctx.Roles.Add( new Role { Name = "User" });
            ctx.Roles.Add( new Role { Name = "Administrator" });
            var role4 = ctx.Roles.Add( new Role { Name = "SuperAdministrator" }).Entity;

            ctx.Users.Add(
                new User()
                {
                    UserName = "timmy3",
                    Email = "timmy3@inko.dk",
                    PasswordHash = "AQAAAAEAACcQAAAAEEi5SaGp0VvXCjSBkDleGXTxVV8fEEaEs+vPEXKmQOzBZiVqTn8kSvaNiXc07txrxQ==",
                    Role = role1
                }
            );
            ctx.Users.Add(
                new User()
                {
                    UserName = "lbilde",
                    Email = "urf@easv.dk",
                    PasswordHash = "AQAAAAEAACcQAAAAEKDwmbRrtQpiaZ22H6Awcpp4pRlOZGo3fSqcvRE3WsyMVOJ4sJEEqXRuDJzEsSJUtA==",
                    Role = role4
                }
            );
            ctx.SaveChanges();
            
            ctx.SaveChanges();
        }
    }
}