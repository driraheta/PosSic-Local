using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class HistoricoVentasViewModel
    {
       
        public string CodVendedor { get; set; }
 
        public int Mes { get; set; }

       
        public int Año { get; set; }

        public int UnidVentas { get; set; }

    
        public decimal MtoVentas { get; set; }
 
        public decimal CostoPro { get; set; }
    }
}
