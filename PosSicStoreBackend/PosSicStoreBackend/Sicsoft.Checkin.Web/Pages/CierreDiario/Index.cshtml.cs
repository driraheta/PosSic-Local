using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.CierreDiario
{
    public class IndexModel : PageModel
    {
        private readonly ICrudApi<CierreDiarioViewModel, int> service;
        private readonly ICrudApi<ParametrosViewModel, string> param;


        [BindProperty]
        public CierreDiarioViewModel Cierre { get; set; }

        [BindProperty]
        public ParametrosViewModel[] Parametros { get; set; }

        public IndexModel(ICrudApi<CierreDiarioViewModel, int> service, ICrudApi<ParametrosViewModel, string> param)
        {
            this.service = service;
            this.param = param;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Cierre = new CierreDiarioViewModel();
                Parametros = await param.ObtenerLista("");
                Cierre.Fecha = Parametros.FirstOrDefault().FechaSistema;

                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                 

                await service.Agregar(Cierre);
                // return RedirectToPage("./Index");
                bool success = true;
                return RedirectToPage("/Index",new { success = success });
                //return new JsonResult(true);
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
