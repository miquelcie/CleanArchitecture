using Customers.Application.Customers.Queries;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Customers.Web.Models
{
    public class CustomerModel
    {
        public MultiSelectList TiposIntereses { get; set; }

        public CustomerVM CurrentCustomer { get; set; }

        public int[] SelectedValues { get; set; }
    }
}