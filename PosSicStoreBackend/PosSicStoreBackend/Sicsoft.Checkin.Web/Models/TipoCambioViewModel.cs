using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class TipoCambioViewModel
    {
        [Key]
        public DateTime Fecha { get; set; }

        [Column("TipoCambio", TypeName = "money")]
        public decimal? TipoCambio1 { get; set; }
    }
}
