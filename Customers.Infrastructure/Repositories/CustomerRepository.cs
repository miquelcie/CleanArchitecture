using Customers.Core.Entities;
using Customers.Core.Repositories;
using Customers.Core.Repositories.Base;
using Customers.Infrastructure.Data;
using Customers.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
       
        public CustomerRepository(CustomerContext customerContext):base(customerContext)
        {
            
        }
        public async Task<IEnumerable<Customer>> GetCustomersByName(string customerName)
        {
            return await _customerContext.Customers.Where(x => x.Nombre == customerName).ToListAsync();
        }
    }
}
