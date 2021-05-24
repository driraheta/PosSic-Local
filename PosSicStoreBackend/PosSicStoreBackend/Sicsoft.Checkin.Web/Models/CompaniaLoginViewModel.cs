using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class CompaniaLoginViewModel
    {
        public CompañiaViewModel Compañia { get; set; }
        public DateTime FechaSistema { get; set; }
        public string token { get; set; }
    }
}
