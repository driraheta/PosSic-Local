using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class VentasXHoraViewModel
    {
       
            public decimal Subtotal { get; set; }
            public decimal Descuento { get; set; }
            public decimal Impuestos { get; set; }
            public decimal Total { get; set; }
            public DateTime FecFactura { get; set; }
            public string Hora { get; set; }

     
    }
}
