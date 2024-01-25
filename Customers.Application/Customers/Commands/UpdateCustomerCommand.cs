using Customers.Application.Common.Interfaces;
using Customers.Application.Customers.Queries;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
using InteresesQ.Application.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<int>
    {


        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string NombreEmpresa { get; set; }
        public string CuentaTwitter { get; set; }
        public List<InteresVM> Intereses { get; set; }
        public Gender Gender { get; set; }

        public UpdateCustomerCommand(CustomerVM customer)
        {
            Nombre = customer.Nombre;
            Apellidos = customer.Apellidos;           
            DocumentoIdentificacion = customer.DocumentoIdentificacion;
            Email = customer.Email;
            FechaNacimiento = customer.FechaNacimiento;
            Pais = customer.Pais;
            Telefono = customer.Telefono;
            NombreEmpresa = customer.NombreEmpresa;
            CuentaTwitter = customer.CuentaTwitter;
            Intereses = customer.Intereses;
            Gender = customer.Gender;
        }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
        {
            private readonly ICustomerDbContext dbContext;
            private readonly IMediator mediator;
            private readonly ICurrentCustomer currentCustomer;

            public UpdateCustomerCommandHandler(ICustomerDbContext dbContext, IMediator mediator, ICurrentCustomer currentCustomer)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
                this.currentCustomer = currentCustomer;
            }

            public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer customer = dbContext.Customers.Find(currentCustomer.Id);


                customer.Nombre = request.Nombre;
                customer.Apellidos = request.Apellidos;
                customer.DocumentoIdentificacion = request.DocumentoIdentificacion;
                customer.Email = request.Email;
                customer.FechaNacimiento = request.FechaNacimiento;
                customer.Pais = request.Pais;
                customer.Telefono = request.Telefono;

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
                    dbContext.Attach(interes);                    
                    customer.Intereses.Add(interes);
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                return currentCustomer.Id;
            }
        }
    }
}
