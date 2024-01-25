using Customers.Application.Common.Interfaces;
using Customers.Application.Customers.Queries;
using Customers.Application.Emails.Commands;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
using InteresesQ.Application.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Application.Customers.Commands
{
    public class UpdateExtraCustomerCommand : IRequest<int>
    {

        public string NombreEmpresa { get; set; }
        public string CuentaTwitter { get; set; }
        public List<InteresVM> Intereses { get; set; }
        public Gender Gender { get; set; }

        public UpdateExtraCustomerCommand(CustomerVM customer)
        {
            NombreEmpresa = customer.NombreEmpresa;
            CuentaTwitter = customer.CuentaTwitter;
            Intereses = customer.Intereses;
            Gender = customer.Gender;
        }

        public class UpdateExtraCustomerCommandHandler : IRequestHandler<UpdateExtraCustomerCommand, int>
        {
            private readonly ICustomerDbContext dbContext;
            private readonly IMediator mediator;
            private readonly ICurrentCustomer currentCustomer;

            public UpdateExtraCustomerCommandHandler(ICustomerDbContext dbContext, IMediator mediator, ICurrentCustomer currentCustomer)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
                this.currentCustomer = currentCustomer;
            }

            public async Task<int> Handle(UpdateExtraCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer customer = dbContext.Customers.Find(currentCustomer.Id);


                customer.NombreEmpresa = request.NombreEmpresa;
                customer.CuentaTwitter = request.CuentaTwitter;
                customer.Gender = request.Gender;

                customer.Intereses.Clear();
                
                foreach (var item in request.Intereses)
                {
                    var interes = new Intereses()
                    {
                        IdInteres = item.IdInteres                      
                    };
                    dbContext.Intereses.Attach(interes);
                    customer.Intereses.Add(interes);
                }
  
                await dbContext.SaveChangesAsync(cancellationToken);

                return currentCustomer.Id;
            }
        }
    }
}
