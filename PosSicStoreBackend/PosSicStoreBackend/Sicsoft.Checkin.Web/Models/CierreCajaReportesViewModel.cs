using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CierreCajaReportesViewModel
    {
        public double NumCierre { get; set; }
        public DateTime Fecha { get; set; }
        public string CodCaja { get; set; }
        public string CodCajero { get; set; }
        public decimal VentaNeta { get; set; }
        public decimal ImpuestoVenta { get; set; }
        public decimal VentaBruta { get; set; }
        public decimal MontoApertura { get; set; }
        public decimal CortesEfectivo { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Tarjetas { get; set; }
        public decimal Dolares { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Diferencia { get; set; }
        public decimal EfectivoF { get; set; }
        public decimal TarjetasF { get; set; }
        public decimal DolaresF { get; set; }
        public int Transacciones { get; set; }
    }
}
