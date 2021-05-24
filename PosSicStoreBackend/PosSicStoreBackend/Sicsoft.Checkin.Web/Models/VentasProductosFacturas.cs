using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class VentasProductosFacturas
    {
        public int NumFactura { get; set; }
        public int NumLinea { get; set; }
        public string CodCajero { get; set; }
        public string CodVendedor { get; set; }
        public string CodPro { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CostoPro { get; set; }
        public decimal Cantidad { get; set; }
        public string Cliente { get; set; }
        public DateTime FecFactura { get; set; }
        public string CodLinea { get; set; }
        public bool Anulada { get; set; }
        public float PorDescto { get; set; }
    }
}
