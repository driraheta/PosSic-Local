using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
 

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Sicsoft.Checkin.Web.Models
{
    public class LoginUsuarioViewModel
    {
        

        public int CodHotel { get; set; }
        
        public int Id { get; set; }

        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }
     
    }
}
