using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class ComprasReportes
    {
        public int NumCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public DetComprasViewModel[] DetCompras { get; set; }
    }
}
