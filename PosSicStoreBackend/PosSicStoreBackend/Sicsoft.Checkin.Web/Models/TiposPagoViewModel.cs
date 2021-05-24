using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class TiposPagoViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodPago { get; set; }

        [StringLength(30)]
        public string NomPago { get; set; }
    }
}
