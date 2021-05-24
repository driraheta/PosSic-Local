using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CodigosTarifaImpuestosViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodTarifa { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public bool? Activo { get; set; }
    }
}
