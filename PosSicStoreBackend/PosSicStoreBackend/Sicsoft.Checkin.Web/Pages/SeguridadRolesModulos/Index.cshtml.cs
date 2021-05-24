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
using Sicsoft.Checkin.Web.Models.Inventario;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.SeguridadRolesModulos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<SeguridadRoles, int> roles;
        private readonly ICrudApi<SeguridadModulos, int> modulos;
        private readonly ICrudApi<SeguridadRolesModulosViewModel, int> rolesMod;

        [BindProperty]
        public SeguridadRoles[] Roles { get; set; }

        [BindProperty]
        public SeguridadModulos[] ModulosMios { get; set; }

        [BindProperty]
        public SeguridadModulos[] Modulos { get; set; }

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        public IndexModel(ICrudApi<SeguridadRoles, int> roles, ICrudApi<SeguridadModulos, int> modulos, ICrudApi<SeguridadRolesModulosViewModel, int> rolesMod)
        {
            this.roles = roles;
            this.modulos = modulos;
            this.rolesMod = rolesMod;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Roles = await roles.ObtenerLista(filtro);
               
 

                return Page();
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }


        public async Task<IActionResult> OnGetEliminar(string id)
        {
            try
            {

                await roles.Eliminar(id);
                return new JsonResult(true);
            }
            catch (ApiException ex)
            {
                return new JsonResult(false);
            }
        }

    }
}
