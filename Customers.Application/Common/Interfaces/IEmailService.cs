using Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(string emailAddress, string body);
    }
}
