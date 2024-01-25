using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.Domain.Entities
{
    [Table("Intereses")]
    public class Intereses 
    {
        public Intereses() { 
        
            this.Customers = new HashSet<Customer>();
        }

        [Key]
        public int IdInteres { get; set; }

            
        public string Nombre { get; set; }

        public  ICollection<Customer> Customers { get; set; }


    }
}