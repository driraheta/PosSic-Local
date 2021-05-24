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

namespace Sicsoft.Checkin.Web.Pages.TiposMovimientos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<TiposMovimientosViewModel, string> service;

        [BindProperty]
        public TiposMovimientosViewModel Tipo { get; set; }
        public TiposMovimientosViewModel[] Tipos { get; set; }

        public EditarModel(ICrudApi<TiposMovimientosViewModel, string> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {

                Tipo = await service.ObtenerPorId(id);

                if(Tipo.Conversion == -1)
                {
                    Tipo.Salida = true;
                    Tipo.Entrada = false;
                }
                else
                {
                    Tipo.Salida = false;
                    Tipo.Entrada = true;
                }
                Tipos = await service.ObtenerLista("");
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
                if (Tipo.Entrada == true)
                {
                    Tipo.Conversion = 1;
                }
                else if (Tipo.Salida)
                {
                    Tipo.Conversion = -1;
                }
                await service.Editar(Tipo);
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
