using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Sicsoft.Checkin.Web.Pages.Account
{
    public class CambiarContraseñaModel : PageModel
    {
        private readonly ICrudApi<SeguridadUsuarioViewModel, string> Usuarios;

        [BindProperty]
        public SeguridadUsuarioViewModel Usuario { get; set; }

        public CambiarContraseñaModel(ICrudApi<SeguridadUsuarioViewModel, string> Usuarios)
        {
            this.Usuarios = Usuarios;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var Email = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.Email).Select(s1 => s1.Value).FirstOrDefault();
                Usuario = await Usuarios.ObtenerPorId(Email);

                return Page();
            }
            catch (ApiException ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                
                await Usuarios.Editar(Usuario);
                return RedirectToPage("/Index");
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }
    }
}
