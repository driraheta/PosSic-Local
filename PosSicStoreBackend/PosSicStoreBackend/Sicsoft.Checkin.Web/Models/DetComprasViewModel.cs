using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class DetComprasViewModel
    {
     
        public int NumCompra { get; set; }

    
        public string CodProveedor { get; set; }

 
        public short NumLinea { get; set; }

         
        public string CodPro { get; set; }

        public decimal Cantidad { get; set; }
 
        public decimal CostoPro { get; set; }

        public decimal? ImptoVta { get; set; }

        public decimal? PorDescuento { get; set; }
    }
}
