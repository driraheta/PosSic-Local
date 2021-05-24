using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.CierreCaja
{
    public class SegundoSelectVentas
    {
        public decimal Total { get; set; }
        public decimal MtoEfectivo { get; set; }
        public decimal MtoTarjeta { get; set; }
        public decimal MtoOtros { get; set; }
    }
}
