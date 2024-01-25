using Customers.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InteresesQ.Application.Customers.Queries
{
    public class GetInteresQuery : IRequest<IEnumerable<InteresVM>>
    {


        public GetInteresQuery()
        {

        }

        public class GetTipoInteresQueryHandler : IRequestHandler<GetInteresQuery, IEnumerable<InteresVM>>
        {
            private readonly ICustomerDbContext dbContext;

            public GetTipoInteresQueryHandler(ICustomerDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<IEnumerable<InteresVM>> Handle(GetInteresQuery request, CancellationToken cancellationToken)
            {
                var _data = await dbContext.Intereses
                    .Select(interes => new InteresVM
                    {
                        IdInteres = interes.IdInteres,
                        Nombre = interes.Nombre
                        
                    })
                    .ToListAsync(cancellationToken);

                return _data;
            }
        }

    }
}
