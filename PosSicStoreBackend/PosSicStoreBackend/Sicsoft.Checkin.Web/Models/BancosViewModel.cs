using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class BancosViewModel
    {
        [Key]
        [StringLength(2)]
        public string CodBanco { get; set; }

        [Required]
        [StringLength(30)]
        public string NomBanco { get; set; }
    }
}
