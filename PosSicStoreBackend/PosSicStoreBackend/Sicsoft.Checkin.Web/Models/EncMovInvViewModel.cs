using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class EncMovInvViewModel
    {
    
    
        public int NumDocto { get; set; }

        
        [StringLength(2)]
        public string CodMov { get; set; }

        [StringLength(50)]
        public string Detalle { get; set; }

        public DateTime FecDocto { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalMov { get; set; }

        public string Proveedor { get; set; }
    }
}
