using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class DetMovInvViewModel
    {
  
        public int NumDocto { get; set; }

   
        [StringLength(2)]
        public string CodMov { get; set; }

      
        public short NumLinea { get; set; }

        [Required]
        [StringLength(10)]
        public string CodPro { get; set; }

        public string NomPro { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public int Cantidad { get; set; }
    }
}
