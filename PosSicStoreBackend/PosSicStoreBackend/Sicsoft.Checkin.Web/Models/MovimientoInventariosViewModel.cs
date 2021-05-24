using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Models
{
    public class MovimientoInventariosViewModel
    {
        public EncMovInvViewModel EncMovInv { get; set; }
        public DetMovInvViewModel[] detMovInv { get; set; }
        public HistInvViewModel[] HistoInv { get; set; }
    }
}
