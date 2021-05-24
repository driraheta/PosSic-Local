using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CajasViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodCaja { get; set; }

        [Required]
        [StringLength(20)]
        public string NomCaja { get; set; }

        [StringLength(20)]
        public string SerCpu { get; set; }

        [StringLength(20)]
        public string SerTec { get; set; }

        [StringLength(20)]
        public string SerImp { get; set; }

        [StringLength(20)]
        public string SerMon { get; set; }

        [StringLength(20)]
        public string ModCpu { get; set; }

        [StringLength(20)]
        public string ModTec { get; set; }

        [StringLength(20)]
        public string ModImp { get; set; }

        [StringLength(20)]
        public string ModMon { get; set; }
    }
}
