using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.Usuarios
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<SeguridadUsuarioViewModel, string> service;
        private readonly ICrudApi<SeguridadRoles, int> roles;

        [BindProperty]
        public SeguridadUsuarioViewModel Usuario { get; set; }

        [BindProperty]
        public SeguridadRoles[] Roles { get; set; }

        public EditarModel(ICrudApi<SeguridadUsuarioViewModel, string> service, ICrudApi<SeguridadRoles, int> roles)
        {
            this.service = service;
            this.roles = roles;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                Roles = await roles.ObtenerLista("");
                Usuario = await service.ObtenerPorId(id);
                return Page();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await service.Editar(Usuario);
                return RedirectToPage("./Index");
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
