using Customers.Application.Common.Interfaces;
using Customers.Application.Customers.Queries;
using Customers.Application.Emails.Commands;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
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
    public class DeleteCustomerCommand : IRequest<int>
    {


        public DeleteCustomerCommand()
        {
           
        }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
        {
            private readonly ICustomerDbContext dbContext;
            private readonly IMediator mediator;
            private readonly ICurrentCustomer currentCustomer;

            public DeleteCustomerCommandHandler(ICustomerDbContext dbContext, IMediator mediator, ICurrentCustomer currentCustomer)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
                this.currentCustomer = currentCustomer;
            }

            public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer customer = dbContext.Customers.Find(currentCustomer.Id);


                dbContext.Customers.Remove(customer);

                await dbContext.SaveChangesAsync(cancellationToken);

                return currentCustomer.Id;
            }
        }
    }
}
