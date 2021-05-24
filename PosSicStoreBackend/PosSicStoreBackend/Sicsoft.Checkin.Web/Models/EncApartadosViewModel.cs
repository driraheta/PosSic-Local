using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class EncApartadosViewModel
    {
        [Key]
        public int NumApartado { get; set; }

        [Required]
        [StringLength(15)]
        public string CodCliente { get; set; }

        public DateTime FecApartado { get; set; }

        public DateTime FecVence { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public decimal? ImptoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal? Descuento { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [Column(TypeName = "money")]
        public decimal AbonoCompra { get; set; }

        [StringLength(2)]
        public string CodVendedor { get; set; }

        public List<DetApartadoViewModel> Detalle { get; set; }
    }
}
