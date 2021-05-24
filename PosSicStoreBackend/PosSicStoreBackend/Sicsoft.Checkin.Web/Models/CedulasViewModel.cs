using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CedulasViewModel
    {
        [Key]
        [StringLength(12)]
        public string CEDULA { get; set; }

        [StringLength(40)]
        public string APELLIDOS { get; set; }

        [StringLength(40)]
        public string NOMBRE { get; set; }

        [StringLength(10)]
        public string Cedula2 { get; set; }
        public int tipo { get; set; }
    }
}
