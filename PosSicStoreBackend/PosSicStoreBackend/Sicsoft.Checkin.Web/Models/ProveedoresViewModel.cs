using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class ProveedoresViewModel
    {
        [Key]
        [StringLength(3)]
        public string CodProveedor { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(30)]
        public string NomProveedor { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Encargado { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoCompras { get; set; }

        public DateTime? FecUltCompra { get; set; }
    }
}
