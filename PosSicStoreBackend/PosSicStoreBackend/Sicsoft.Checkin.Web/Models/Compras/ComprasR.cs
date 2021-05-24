using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Compras
{
    public class ComprasR
    {
        public EncCompras EncCompras { get; set; }
        public DetCompras[] DetCompras { get; set; }
    }
}
