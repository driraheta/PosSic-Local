using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models.Inventario
{
    public class Recibido
    {
        public EncMovInv encMovInv { get; set; }
        public Productos[] productos { get; set; }
    }
}
