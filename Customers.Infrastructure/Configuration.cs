using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Domain.Entities;

namespace Customers.Infrastructure
{
    public sealed class Configuration : DbMigrationsConfiguration<CustomerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CustomerDbContext context)
        {
            // Agregar datos predeterminados a la tabla
            context.Intereses.AddOrUpdate(
                new Intereses { Nombre = "Deportes", IdInteres=1 },
                new Intereses { Nombre = "Literatura", IdInteres = 2 },
                new Intereses { Nombre = "Cine", IdInteres = 3 },
                new Intereses { Nombre = "Juegos", IdInteres = 4 },
                new Intereses { Nombre = "Formación", IdInteres = 5 }
            );
        }
    }
}
