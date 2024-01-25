using Customers.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure.Data
{
    public class CustomerContextSeed
    {
        public static async Task SeedAsync(CustomerContext customerContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                customerContext.Database.CreateIfNotExists();

                if (!customerContext.Customers.Any())
                {
                    customerContext.Customers.AddRange(GetCustomers());
                    await customerContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CustomerContextSeed>();
                    log.LogError($"Exception occured while connecting: {ex.Message}");
                    await SeedAsync(customerContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
        {
            new Customer { Nombre = "Miquel", Apellidos="Gonzalez"}
           
        };
        }
    }
}
