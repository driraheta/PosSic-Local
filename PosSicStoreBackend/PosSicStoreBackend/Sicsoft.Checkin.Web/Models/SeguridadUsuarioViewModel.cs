using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class SeguridadUsuarioViewModel
    {
        [Required]
        [StringLength(3)]
        public string CodCompania { get; set; }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string NomUsuario { get; set; }

        [StringLength(30)]
        public string Clave { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        public int? Codrol { get; set; }
    }
}
