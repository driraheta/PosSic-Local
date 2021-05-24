using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class VendedorViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodVendedor { get; set; }

        [Required]
        [StringLength(30)]
        public string NomVendedor { get; set; }

        public float PorComision { get; set; }
    }
}
