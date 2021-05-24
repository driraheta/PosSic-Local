using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CierreCajaViewModel
    {
        [Key]
       
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Key]
       
        [StringLength(2)]
        public string CodCaja { get; set; }

        [Key]
       
        public DateTime FecCaja { get; set; }

        public DateTime FechaSistema { get; set; }

        [StringLength(15)]
        public string CodSupervisor { get; set; }

       
        public decimal? Efectivo { get; set; }

       
        public decimal? Cheques { get; set; }

      
        public decimal? Tarjetas { get; set; }

     
        public decimal? OtrosPagos { get; set; }

    
        public decimal? MtoVendido { get; set; }

     
        public decimal? Diferencia { get; set; }

        public bool Abierta { get; set; }

        public DateTime? HoraCierre { get; set; }

         
        public decimal? MontoApertura { get; set; }

   
        public decimal? CortesEfectivo { get; set; }

        [StringLength(250)]
        public string CortesDescripcion { get; set; }

        public double? NumCierre { get; set; }

        public bool? Activa { get; set; }

      
        public decimal? Dolares { get; set; }

       
        public decimal? TipoCambio { get; set; }

        [StringLength(50)]
        public string IP { get; set; }
    }
}
