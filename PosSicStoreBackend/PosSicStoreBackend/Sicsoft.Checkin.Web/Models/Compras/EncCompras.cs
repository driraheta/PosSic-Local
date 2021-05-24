using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Compras
{
    public class EncCompras
    {
    
      
        public int NumCompra { get; set; }

       
        [StringLength(3)]
        public string CodProveedor { get; set; }

        public DateTime FechaCompra { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImptoVta { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDescuento { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalCompra { get; set; }
    }
}
