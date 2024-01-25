using Customers.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using System.Collections.Generic;
using Customers.Application.Customers.Queries;
using Customers.Application.Customers.Commands;
using Customers.Domain.Entities;
using System.Linq;
using InteresesQ.Application.Customers.Queries;

namespace Customers.Web.Controllers
{
    public class CustomerController : Controller
    {
        private const string CLIENTES = "Clientes";
        private const string VER_CLIENTE = "Ver cliente";
        private const string ALTA_CLIENTE = "Alta de cliente";
        private const string MODIFICAR_CLIENTE = "Modificar cliente";
        private const string COMPLETAR_CLIENTE = "Completar información del cliente";
        private const string BORRAR_CLIENTE = "Borrar cliente";

        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: Costumer
        public async Task<ActionResult> Index()
        {

            ViewBag.Section = CLIENTES;
            ViewBag.Message = "Lista de clientes";

            var _cmd = new GetCustomersQuery();

            IEnumerable<CustomerVM> customers = await mediator.Send(_cmd);

            return View(customers);
        }

        // GET: Costumer/Details/5


        // GET: Costumer/Create
        public ActionResult Create()
        {
            ViewBag.Section = ALTA_CLIENTE;
            CustomerVM customer = new CustomerVM();
            return View(customer);
        }

        // POST: Costumer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerVM customer)
        {
            try
            {
                var _cmd = new CreateCustomerCommand(customer);

                var _result = await mediator.Send(_cmd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Costumer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Section = MODIFICAR_CLIENTE;

            var _cmd = new GetCustomerQuery(id);

            var currentCustomer = await mediator.Send(_cmd);

            var _cmdInteres = new GetInteresQuery();

            var intereses = await mediator.Send(_cmdInteres);

            int[] selected = new int[] { };
            if (currentCustomer.Intereses != null)
            {
                 selected = currentCustomer.Intereses.Select(x => x.IdInteres).ToArray();
            }


            ViewBag.Section = COMPLETAR_CLIENTE;
            CustomerModel customer = new CustomerModel()
            {
                SelectedValues = selected,
                CurrentCustomer = currentCustomer,
                TiposIntereses = new MultiSelectList(intereses,"IdInteres","Nombre", selected)
            };
            return View(customer);
        }



        // POST: Costumer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CustomerModel customer)
        {
            try
            {
                string[] selectedValues = Request.Form.GetValues("SelectedValues");

                if (selectedValues.Length > 0)
                {

                    customer.CurrentCustomer.Intereses = selectedValues.Select(x => new InteresVM() { IdInteres = int.Parse(x) }).ToList();

                }

                this.HttpContext.Items.Remove("Id");
                this.HttpContext.Items.Add("Id", id);

                var _cmd = new UpdateCustomerCommand(customer.CurrentCustomer);

                var _result = await mediator.Send(_cmd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Costumer/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.Section = BORRAR_CLIENTE;

            var _cmd = new GetCustomerQuery(id);

            var customer = await mediator.Send(_cmd);

            return View(customer);
        }

        // POST: Costumer/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteComplete(int id)
        {
            try
            {
                this.HttpContext.Items.Remove("Id");
                this.HttpContext.Items.Add("Id", id);

                var _cmd = new DeleteCustomerCommand();

                var _result = await mediator.Send(_cmd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
