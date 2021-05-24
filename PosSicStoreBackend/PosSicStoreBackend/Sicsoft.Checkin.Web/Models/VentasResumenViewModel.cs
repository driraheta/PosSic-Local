using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class VentasResumenViewModel
    {
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string FecFactura { get; set; }
    }
}
