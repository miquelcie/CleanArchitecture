using Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Application.Common.Interfaces
{
    public interface ICustomerDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Intereses> Intereses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Attach(Intereses intereses);
    }
}
