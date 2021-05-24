using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CajerosViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Required]
        [StringLength(50)]
        public string NomCajero { get; set; }
 
        [StringLength(30)]
        public string ClavePaso { get; set; }
        public bool Asignar { get; set; }
    }
}

