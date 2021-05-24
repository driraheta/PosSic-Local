using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class TarjetasViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodTarjeta { get; set; }

        [Required]
        [StringLength(30)]
        public string NomTarjeta { get; set; }

        [StringLength(30)]
        public string EnteEmisor { get; set; }

        public float? PorComision { get; set; }
    }
}
