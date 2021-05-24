using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class SeguridadRolesModulosViewModel
    {
        [Key]

        public int CodRol { get; set; }

        public int CodModulo { get; set; }
    }
}
