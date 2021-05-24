using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Compras
{
    public class RecibidoC
    {
        public EncCompras EncCompras { get; set; }
        public ProductosC[] DetCompras { get; set; }
    }
}
