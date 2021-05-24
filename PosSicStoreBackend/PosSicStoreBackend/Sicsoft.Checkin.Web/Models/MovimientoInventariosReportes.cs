using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class MovimientoInventariosReportes
    {
        public int NumDocto { get; set; }
        public string CodMov { get; set; }
        public DateTime FecDocto { get; set; }
        public string Detalle { get; set; }
        public DetMovInvViewModel[] DetMovInv { get; set; }

    }
}
