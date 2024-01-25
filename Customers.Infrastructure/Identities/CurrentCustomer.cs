using Customers.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Customers.Infrastructure.Identities
{
    public class CurrentCustomer : ICurrentCustomer
    {
        public int Id {
            get
            {
                string id = HttpContext.Current?.Items?["Id"]?.ToString() ?? string.Empty;   

                if (!string.IsNullOrEmpty(id))
                    return int.Parse(id);

                return 0;
            }
       }
    }
}
