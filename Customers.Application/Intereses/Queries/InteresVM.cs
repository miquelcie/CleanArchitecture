using Customers.Domain.Entities;
using Customers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteresesQ.Application.Customers.Queries
{
    public class InteresVM : ICloneable
    {
        public int IdInteres { get; set; }

        public string Nombre { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
