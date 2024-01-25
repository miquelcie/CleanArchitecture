using AutoMapper;
using Customers.Application.Common.Interfaces;
using Customers.Domain.Entities;
using InteresesQ.Application.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Application.Customers.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerVM>>
    {

        private readonly string LastNameFilter;

        public GetCustomersQuery()
        {

        }

        public GetCustomersQuery(string lastNameFilter)
        {
            LastNameFilter = lastNameFilter;
        }

        public class GetEmployeesQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerVM>>
        {
            private readonly ICustomerDbContext dbContext;

            public GetEmployeesQueryHandler(ICustomerDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<IEnumerable<CustomerVM>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
            {
                // En algún lugar de la inicialización de la aplicación
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Customer, CustomerVM>().ForMember(dest=>dest.Intereses, opt =>opt.MapFrom(src=>src.Intereses));
                   
                    cfg.CreateMap<Intereses, InteresVM>();
                });


                IMapper mapper = config.CreateMapper();

              
                var data = mapper.Map<List<Customer>, List<CustomerVM>>(await dbContext.Customers.Include(c => c.Intereses).ToListAsync());

                   
                return data;
            }
        }

    }
}
