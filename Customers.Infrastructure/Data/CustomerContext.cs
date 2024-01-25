using Customers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure.Data
{
    public class CustomerContext: DbContext
    {
        public CustomerContext():base("Name=CustomersDBConnection") { }

        public DbSet<Customer> Customers { get; set; }
    }
}
