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

namespace Sicsoft.Checkin.Web.Pages.Lineas
{
    public class NuevoModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<LineasViewModel, string> service;

        [BindProperty]
        public LineasViewModel Linea { get; set; }

        public NuevoModel(ICrudApi<LineasViewModel, string> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Linea = new LineasViewModel();
              var Lineas = await service.ObtenerLista("");
                Linea.CodLinea = (Convert.ToInt32(Lineas.LastOrDefault().CodLinea) + 1).ToString();

                var cantidadCeros = 3 - Linea.CodLinea.Length;

                var Codigo = Linea.CodLinea;

                for(int i = 0; i < cantidadCeros; i++)
                {
                    Codigo = "0" + Codigo;
                }
                Linea.CodLinea = Codigo;




                return Page();
            }
            catch (Exception ex)
            {

                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await service.Agregar(Linea);
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
