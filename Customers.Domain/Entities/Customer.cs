
using Customers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Customers.Domain.Entities
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            this.Intereses = new HashSet<Intereses>();
        }



        [Key]
        public int Id { get; set; }

        [MaxLength(9)]
        [Required]
        public string DocumentoIdentificacion { get; set; }
        
        [MaxLength(155)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(200)]
        [Required]
        public string Apellidos { get; set; }

        [MaxLength(155)]
        [Required]
        public string Email { get; set; }

        [MaxLength(80)]
        [Required]
        public string Pais { get; set; }

        [MaxLength(20)]
        [Required]

        public string Telefono { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(255)]
        public string NombreEmpresa { get; set; }
        
        [MaxLength(60)]
        public string CuentaTwitter { get; set; }

        public Gender Gender { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public virtual ICollection<Intereses> Intereses { get; set; }

       /* public List<int> InteresadosId { 
            get {
                return Intereses.Select(x => x.IdInteres).ToList();
            } 
        }*/      
    }
}
