using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class LineasViewModel
    {
        [Key]
        [StringLength(3)]
        public string CodLinea { get; set; }


        [StringLength(30)]
        public string NomLinea { get; set; }
    }
}
