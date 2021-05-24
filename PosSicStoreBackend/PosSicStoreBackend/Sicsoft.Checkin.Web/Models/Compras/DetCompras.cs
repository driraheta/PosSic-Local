using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Compras
{
    public class DetCompras
    {
        
       
        public int NumCompra { get; set; }

        
        [StringLength(3)]
        public string CodProveedor { get; set; }

    
         
        public short NumLinea { get; set; }

        public string NomPro { get; set; }

        [StringLength(10)]
        public string CodPro { get; set; }

        public decimal Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public decimal? ImptoVta { get; set; }

        public decimal? PorDescuento { get; set; }
    }
}
