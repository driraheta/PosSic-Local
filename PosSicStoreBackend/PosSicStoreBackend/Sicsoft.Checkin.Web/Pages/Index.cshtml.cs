using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Models.CierreCaja;
using Sicsoft.Checkin.Web.Servicios;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICrudApi<TipoCambioViewModel, int> service;

        [BindProperty]
        public bool CierreExitoso { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICrudApi<TipoCambioViewModel, int> service)
        {
            _logger = logger;
            this.service = service;
        }

        public async Task<IActionResult> OnGet(bool success = false)
        {
            try
            {
                CierreExitoso = success;
                return Page();
            }
            catch (Exception ex)
            {

                return Page();
            }
        }

        public async Task<IActionResult> OnGetTipoCambio()
        {
            try
            {
                var TipoCambio = await service.ObtenerPorId("0");
                decimal TipoCambioSugerido = 0;
                object resp;
                if (TipoCambio.TipoCambio1 == null)
                {
                    using(HttpClient httpCliente = new HttpClient())
                    {

                        var time = DateTime.Now;
                        var URL = Constantes.urlwebapi.ToString() + time.Day + "/" + (time.Month < 10 ? "0" + time.Month.ToString() : time.Month.ToString()) + "/" + time.Year;
                        var response2 = await httpCliente.GetAsync(URL); 
                        if (response2.IsSuccessStatusCode)
                        {
                            var result = JsonConvert.DeserializeObject<TipoCambioString>(response2.Content.ReadAsStringAsync().Result);
                    
                             TipoCambioSugerido = Convert.ToInt32(result.venta);
                        }
                        else
                        {
                            TipoCambioSugerido = 0;
                        }
                    }
                    resp = new
                    {
                        success = true,
                        tipocambio = TipoCambioSugerido
                    };
                    return new JsonResult(resp);
                }
                resp = new
                {
                    success = false,
                    tipocambio = 0
                };
                return new JsonResult(resp);
            }
            catch (Exception ex)
            {
               var resp = new
                {
                    success = false,
                    tipocambio = 0
                };
                return new JsonResult(resp);
            }
        }

        public async Task<IActionResult> OnGetTipoCambioSetear(decimal id)
        {
            try
            {
                var TipoCambio = new TipoCambioViewModel();
                TipoCambio.TipoCambio1 = id;
                await service.Agregar(TipoCambio);

                return new JsonResult(true);
            }
            catch (Exception ex)
            {

                return new JsonResult(false);
            }
        }
    }
}
