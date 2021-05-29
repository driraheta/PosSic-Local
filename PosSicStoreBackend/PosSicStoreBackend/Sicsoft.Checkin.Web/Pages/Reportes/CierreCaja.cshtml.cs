using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.Reportes
{
    public class CierreCajaModel : PageModel
    {
        private readonly ICrudApi<CierreCajaReportesViewModel, int> service;
        private readonly ICrudApi<CierreTarjetasReportes, int> service2;
        private readonly ICrudApi<CajasViewModel, string> cajas;
        private readonly ICrudApi<CajerosViewModel, string> cajeros;
        private readonly ICrudApi<TarjetasViewModel, string> tarjetas;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public CierreCajaReportesViewModel[] EncVtas { get; set; }

        [BindProperty]
        public CierreTarjetasReportes[] EncVtas2 { get; set; }
        [BindProperty]
        public CajasViewModel[] Cajas { get; set; }

        [BindProperty]
        public CajerosViewModel[] Cajeros { get; set; }

        [BindProperty]
        public TarjetasViewModel[] Tarjetas { get; set; }

        public CierreCajaModel(ICrudApi<CierreCajaReportesViewModel, int> service, ICrudApi<CierreTarjetasReportes, int> service2, ICrudApi<TarjetasViewModel, string> tarjetas, ICrudApi<CajasViewModel, string> cajas, ICrudApi<CajerosViewModel, string> cajeros)
        {
            this.service = service;
            this.cajas = cajas;
            this.cajeros = cajeros;
            this.service2 = service2;
            this.tarjetas = tarjetas;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DateTime time = new DateTime();

                if (filtro.FechaInicial.Date == time.Date)
                {
                    
                    filtro.FechaInicial = DateTime.Now;
                }
                EncVtas = await service.ObtenerCierre(filtro);                
                Cajas = await cajas.ObtenerLista("");
                Cajeros = await cajeros.ObtenerLista("");
                EncVtas2 = await service2.ObtenerCierreTarjetas(filtro);
                Tarjetas = await tarjetas.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
