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

namespace Sicsoft.Checkin.Web.Pages.TiposMovimientos
{
    public class NuevoModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<TiposMovimientosViewModel, string> service;

        [BindProperty]
        public TiposMovimientosViewModel Tipo { get; set; }
        public TiposMovimientosViewModel[] Tipos { get; set; }

        public NuevoModel(ICrudApi<TiposMovimientosViewModel, string> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Tipos = await service.ObtenerLista("");
                Tipo = new TiposMovimientosViewModel();
                Tipo.CodMov = ((int.Parse(Tipos.LastOrDefault().CodMov) + 1) > 9 ? (int.Parse(Tipos.LastOrDefault().CodMov) + 1).ToString() : "0" + (Tipos.Count() + 1).ToString());

                    return Page();

            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

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
                await service.Agregar(Tipo);
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
