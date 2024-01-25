using Customers.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure
{
    public class GMailService : IMailService
    {
        public async Task SendEmail(string emailAddress, string body)
        {
            //Codigo de envio de emaail de GMAIL
        }
    }
   
}
