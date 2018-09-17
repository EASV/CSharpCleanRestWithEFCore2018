using System;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.Data;
using CustomerApp.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EASV.CustomerRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerAppContext>(
                opt => opt.UseInMemoryDatabase("ThaDB")
                );
            
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<CustomerAppContext>();
                    var cust1 = ctx.Customers.Add(new Customer()
                    {
                        Id = 1,
                        Address = "BongiStreet",
                        FirstName = "John",
                        LastName = "Olesen"
                    }).Entity;
                    
                    ctx.Customers.Add(new Customer()
                    {
                        Id = 2,
                        Address = "BongiStreet 22",
                        FirstName = "Bill",
                        LastName = "Bøllesen"
                    });
                    
                    ctx.Orders.Add(new Order()
                    {
                        Id = 1,
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.Now,
                        Customer = cust1
                    });
                    ctx.SaveChanges();
                }
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
