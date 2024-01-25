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
    public class CreateCustomerCommand : IRequest<int>
    {
       
        
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }


        public CreateCustomerCommand(CustomerVM customer)
        {
            Nombre = customer.Nombre;
            Apellidos = customer.Apellidos;
            Gender = customer.Gender;
            DocumentoIdentificacion = customer.DocumentoIdentificacion;
            Email = customer.Email;
            FechaNacimiento = customer.FechaNacimiento;
            Pais = customer.Pais;
            Telefono = customer.Telefono;
        }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly ICustomerDbContext dbContext;
            private readonly IMediator mediator;
            private readonly ICurrentCustomer currentUser;

            public CreateCustomerCommandHandler(ICustomerDbContext dbContext, IMediator mediator, ICurrentCustomer currentUser)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
                this.currentUser = currentUser;
            }

            public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer _newUser = new Customer
                {
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    Gender = request.Gender,
                    DocumentoIdentificacion = request.DocumentoIdentificacion,
                    Email = request.Email,
                    FechaNacimiento = request.FechaNacimiento,
                    Pais = request.Pais,
                    Telefono = request.Telefono,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = currentUser.Id
                };

                dbContext.Customers.Add(_newUser);
                await dbContext.SaveChangesAsync(cancellationToken);

                var body = $"Complete su información: https://localhost:44315/Customer/Edite/{_newUser.Id}";
                var _emailCmd = new SendEmailCommand(_newUser.Email, body);
                await mediator.Send(_emailCmd);

                return _newUser.Id;
            }
        }
    }
}
