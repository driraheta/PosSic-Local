using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class KardexViewModel
    {
        public DateTime Fecha { get; set; }
        public string NumDocto { get; set; }
        public string CodMov { get; set; }
        public string NomMov { get; set; }
        public decimal Costo { get; set; }
        public int Salidas { get; set; }
        public int Entradas { get; set; }
        public string CodPro { get; set; }
    }
}
