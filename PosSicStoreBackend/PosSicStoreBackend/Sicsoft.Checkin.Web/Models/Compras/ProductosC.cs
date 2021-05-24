using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Compras
{
    public class ProductosC
    {
        public string codPro { get; set; }
        public string NomPro { get; set; }
        public decimal PreVta { get; set; }
        public int cantidad { get; set; }
        public int descuento { get; set; }
        public int impuesto { get; set; }
    }
}
