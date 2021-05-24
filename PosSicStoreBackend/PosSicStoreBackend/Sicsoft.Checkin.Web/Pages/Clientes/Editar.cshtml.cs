using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<ClientesViewModel, string> service;
        private readonly ICrudApi<TiposPreciosViewModel, int> serviceTiposPrecios;
        private readonly ICrudApi<CedulasViewModel, string> cedulasService;

        [BindProperty]
        public ClientesViewModel Cliente { get; set; }
        [BindProperty]
        public TiposPreciosViewModel[] TiposPrecios { get; set; }

        public EditarModel(ICrudApi<ClientesViewModel, string> service, ICrudApi<TiposPreciosViewModel, int> serviceTiposPrecios, ICrudApi<CedulasViewModel, string> cedulasService)
        {
            this.service = service;
            this.serviceTiposPrecios = serviceTiposPrecios;
            this.cedulasService = cedulasService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                id = id.Replace("_", "?").Replace("[", "¿").Replace("#", "/");
                Cliente = await service.ObtenerPorId(id);
                TiposPrecios = await serviceTiposPrecios.ObtenerLista("");
                return Page();
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await service.Editar(Cliente);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);
                return Page();
            }
        }
        public async Task<IActionResult> OnGetCedula(string id)
        {
            try
            {
                var Cedula = await cedulasService.ObtenerPorId(id);
                return new JsonResult(Cedula);
            }
            catch (ApiException ex)
            {

                return new JsonResult(ex);
            }
        }
    }
}
