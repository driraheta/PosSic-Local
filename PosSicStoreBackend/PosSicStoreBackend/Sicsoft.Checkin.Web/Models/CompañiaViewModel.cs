using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CompañiaViewModel
    {

        public string CodCompania { get; set; }
        public string NomCompania { get; set; }
        public string Cedula { get; set; }
        public bool Activo { get; set; }
        public string Ubicacion { get; set; }
    }
}
