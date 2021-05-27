using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class DetApartadoViewModel
    {
        [Key]
        [Column(Order = 0)]
       
        public int NumApartado { get; set; }

        [Key]
        [Column(Order = 1)]
  
        public short NumLinea { get; set; }

        [Required]
        [StringLength(10)]
        public string CodPro { get; set; }

        public decimal Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal PrecioVenta { get; set; }

        [Column(TypeName = "money")]
        public decimal? PorDescto { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoPro { get; set; }

        [StringLength(20)]
        public string CodProHacienda { get; set; }
    }
}
