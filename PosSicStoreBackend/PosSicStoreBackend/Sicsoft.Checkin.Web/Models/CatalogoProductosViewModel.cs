using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CatalogoProductosViewModel
    {
        public string CodPro { get; set; }
        public string NomPro { get; set; }
        public string CodLinea { get; set; }
        public string CodBarras { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal ImpuestoTarifa { get; set; }
        public long Existencia { get; set; }
    }
}
