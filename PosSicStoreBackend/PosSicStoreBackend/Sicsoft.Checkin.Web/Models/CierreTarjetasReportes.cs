using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CierreTarjetasReportes
    {
        public double NumCierre { get; set; }
        public DateTime Fecha { get; set; }
        public string CodCaja { get; set; }
        public string CodCajero { get; set; }
        public EncVtasViewModel[] EncVtas { get; set; }
    }
}
