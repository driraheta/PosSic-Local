using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class SeguridadRoles
    {
        
        public int CodRol { get; set; }
 
        [StringLength(50)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
