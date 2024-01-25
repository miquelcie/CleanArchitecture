using Autofac;
using Customers.Infrastructure.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure
{
    public static class DependencyInjection
    {
        public static ContainerBuilder AddInfrastructure(this ContainerBuilder container)
        {
            container.RegisterType<CustomerDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterType<CurrentCustomer>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            container.RegisterType<GMailService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return container;
        }
    }
}
