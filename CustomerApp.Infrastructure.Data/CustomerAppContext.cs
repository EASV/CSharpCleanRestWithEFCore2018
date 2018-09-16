using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data
{
    public class CustomerAppContext: DbContext
    {
        public CustomerAppContext(DbContextOptions<CustomerAppContext> opt) 
            : base(opt) { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}