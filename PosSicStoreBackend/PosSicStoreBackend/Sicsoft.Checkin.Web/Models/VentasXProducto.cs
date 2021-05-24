using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class VentasXProducto
    {
        public decimal Unidades { get; set; }
        public decimal Utilidad { get; set; }
        public decimal TotalCosto { get; set; }
        public decimal CostoPro { get; set; }
        public decimal Total { get; set; }
        public string CodPro { get; set; }
        public string CodLinea { get; set; }
    }
}
