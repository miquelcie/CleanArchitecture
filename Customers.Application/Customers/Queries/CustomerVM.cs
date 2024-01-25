using Customers.Domain.Entities;
using Customers.Domain.Enums;
using InteresesQ.Application.Customers.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Application.Customers.Queries
{
    public class CustomerVM
    {

        public int Id { get; set; }

        [Display(Name = "DNI")]
        [Required]
        public string DocumentoIdentificacion { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [Required]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }



        [Display(Name = "País")]
        [Required]
        public string Pais { get; set; }

        [Display(Name = "Teléfono")]
        [Required]
        public string Telefono { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required]
        public DateTime FechaNacimiento { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Nombre de empresa")]
        [MaxLength(255)]
        public string NombreEmpresa { get; set; }

        [Display(Name = "Cuenta de X")]
        [MaxLength(60)]
        public string CuentaTwitter { get; set; }

        [Display(Name = "Intereses")]
        public List<InteresVM> Intereses { get; set; }

     

        public string InteresesSeparated
        {
            get
            {
                if (Intereses==null) return string.Empty;

                return string.Join(", ", Intereses.Select(x=>x.Nombre));
            }
        }

    }
}
