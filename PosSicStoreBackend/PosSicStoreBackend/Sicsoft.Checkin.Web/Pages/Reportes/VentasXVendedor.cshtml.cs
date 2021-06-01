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
    public class VentasXVendedorModel : PageModel
    {
        private readonly ICrudApi<HistoricoVentasReportes, int> service;
        private readonly ICrudApi<HistoricoVentasViewModel, string> hist;
        private readonly ICrudApi<VendedorViewModel, string> vendedores;
    
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public HistoricoVentasReportes[] EncVtas { get; set; }

        [BindProperty]
        public List<int> Historico { get; set; }

        [BindProperty]
        public VendedorViewModel[] Vendedores { get; set; }

        [BindProperty]
        public int[] Años { get; set; }


        public VentasXVendedorModel(ICrudApi<HistoricoVentasReportes, int> service, ICrudApi<VendedorViewModel, string> vendedores, ICrudApi<HistoricoVentasViewModel, string> hist)
        {
            this.service = service;
            this.vendedores = vendedores;
            this.hist = hist;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (PageContext.HttpContext.Request.Query.Count > 0)
                {
                    EncVtas = await service.ObtenerHVentas(filtro);
                }
                else
                {
                    EncVtas = new HistoricoVentasReportes[0];
                }

                
                Vendedores = await vendedores.ObtenerLista("");
                var Primero = await hist.ObtenerLista("");
              
                var vector = Primero.Select(a => a.Año).ToArray();
                var distinct = vector.Distinct();
                Historico = distinct.ToList();


                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
