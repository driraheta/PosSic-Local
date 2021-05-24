using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class TiposPreciosViewModel
    {
        [Key]
        
        public int CodPrecio { get; set; }

        [StringLength(50)]
        public string NomLista { get; set; }
    }
}
