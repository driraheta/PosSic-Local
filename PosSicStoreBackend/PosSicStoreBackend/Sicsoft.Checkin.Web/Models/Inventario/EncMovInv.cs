using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Inventario
{
    public class EncMovInv
    {
       // public int NumDocto { get; set; }
        public string CodMov { get; set; }
        public string Detalle { get; set; }
        public DateTime FecDocto { get; set; }
        public decimal TotalMov { get; set; }
        public string CodProveedor { get; set; }
    }
}
