using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.Static.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2017
{
    

    class Program
    {
        
        #region Main

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            ////then build provider 
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.StartUI();
        }
        #endregion


    }
}
