using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class ReportesProductosViewModel
    {
        public string CodPro { get; set; }
        public string CodLinea { get; set; }
        public string NomPro { get; set; }
        public short InvInicial { get; set; }
        public int UnidEntrada { get; set; }
        public int UnidCompras { get; set; }
        public int UnidSalida { get; set; }
        public int UnidVentas { get; set; }
        public long Existencia { get; set; }
        public decimal Total { get; set; }
    }
}
