using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class TiposMovimientosViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodMov { get; set; }

        [Required]
        [StringLength(30)]
        public string NomMov { get; set; }

        public float Conversion { get; set; }

        public int NumDoctoSig { get; set; }

        public bool Entrada { get; set; }

        public bool Salida { get; set; }

        public bool Modifica { get; set; }
    }
}
