using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class SeguridadModulos
    {
        public int CodModulo { get; set; }

    
        [StringLength(150)]
        public string Descripcion { get; set; }
    }
}
