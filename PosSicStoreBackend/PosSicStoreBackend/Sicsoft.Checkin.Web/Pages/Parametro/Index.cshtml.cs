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

namespace Sicsoft.Checkin.Web.Pages.Parametros
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<ParametrosViewModel, string> service;
        private readonly ICrudApi<TipoCambioViewModel, int> serviceT;

        [BindProperty]
        public ParametrosViewModel Objeto { get; set; }

        [BindProperty]
        public TipoCambioViewModel TC { get; set; }

        public IndexModel(ICrudApi<ParametrosViewModel, string> service, ICrudApi<TipoCambioViewModel, int> serviceT) 
        {
            this.service = service;
            this.serviceT = serviceT;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {

                Objeto = await service.ObtenerPorId(id);
                TC = await serviceT.ObtenerPorId("0");
                TC.TipoCambio1 = (TC.TipoCambio1 == null ? 0 : Convert.ToInt32(TC.TipoCambio1));
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
                await service.Editar(Objeto);
                var tipoCambio = await serviceT.ObtenerPorId("0");
                if(tipoCambio.TipoCambio1 == null)
                {
                    await serviceT.Agregar(TC);
                }
                else
                {
                    await serviceT.Editar(TC);
                }
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
