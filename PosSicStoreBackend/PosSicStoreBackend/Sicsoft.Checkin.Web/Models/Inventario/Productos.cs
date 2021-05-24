using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Inventario
{
    public class Productos
    {
        public string codPro { get; set; }
        public string NomPro { get; set; }
        public decimal PreVta { get; set; }
        public int cantidad { get; set; }

    }
}
