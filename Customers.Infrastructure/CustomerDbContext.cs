using Customers.Application.Common.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure
{
    public class CustomerDbContext : DbContext, ICustomerDbContext
    {
        public CustomerDbContext()
            : base("name=CustomersDBConnection")
        {
            
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CustomerDbContext, Configuration>());
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Intereses> Intereses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(c => c.Intereses).WithMany(c => c.Customers);
            base.OnModelCreating(modelBuilder);

           
        }

        public void Attach(Intereses intereses) {
            if (this.Entry(intereses).State == EntityState.Detached) 
                this.Intereses.Attach(intereses);
           
        }



    }
    
    
}
