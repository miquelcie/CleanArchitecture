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
    public class GetCustomerQuery : IRequest<CustomerVM>
    {

        private readonly int Id;

        public GetCustomerQuery()
        {

        }

        public GetCustomerQuery(int Id)
        {
            this.Id = Id;
        }

        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerVM>
        {
            private readonly ICustomerDbContext dbContext;

            public GetCustomerQueryHandler(ICustomerDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<CustomerVM> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                // En algún lugar de la inicialización de la aplicación
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Customer, CustomerVM>().ForMember(dest => dest.Intereses, opt => opt.MapFrom(src => src.Intereses));

                    cfg.CreateMap<Intereses, InteresVM>();
                });


                IMapper mapper = config.CreateMapper();


                var data = mapper.Map<Customer, CustomerVM>(await dbContext.Customers.Include(c => c.Intereses).SingleAsync(x=>x.Id == request.Id));


                return data;


               
            }
        }

    }
}
