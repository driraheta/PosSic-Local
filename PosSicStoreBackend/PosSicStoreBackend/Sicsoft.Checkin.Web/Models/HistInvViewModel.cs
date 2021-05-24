using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class HistInvViewModel
    {
      
        [StringLength(2)]
        public string CodMov { get; set; }

        
        [StringLength(50)]
        public string NumDocto { get; set; }
 
        [StringLength(15)]
        public string CodPro { get; set; }

        
    
        public int NumLinea { get; set; }

        [StringLength(2)]
        public string CodBodega { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        [StringLength(3)]
        public string CodProveedor { get; set; }
    }
}
