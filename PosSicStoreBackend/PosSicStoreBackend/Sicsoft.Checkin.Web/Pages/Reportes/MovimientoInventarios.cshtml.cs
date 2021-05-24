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
    public class MovimientoInventariosModel : PageModel
    {
        private readonly ICrudApi<MovimientoInventariosReportes, int> service;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        private readonly ICrudApi<TiposMovimientosViewModel, string> lineas;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public MovimientoInventariosReportes[] EncVtas { get; set; }

        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        [BindProperty]
        public TiposMovimientosViewModel[] CodMov { get; set; }

        public MovimientoInventariosModel(ICrudApi<MovimientoInventariosReportes, int> service, ICrudApi<ProductosViewModel, string> productos, ICrudApi<TiposMovimientosViewModel, string> lineas)
        {
            this.service = service;
            this.productos = productos;
            this.lineas = lineas;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DateTime time = new DateTime();

                if (filtro.FechaFinal.Date == time.Date)
                {
                    filtro.FechaInicial = DateTime.Now.AddMonths(-2);
                    filtro.FechaFinal = DateTime.Now;
                }
                filtro.FechaFinal = filtro.FechaFinal.AddDays(1);

                EncVtas = await service.ObtenerMovimiento(filtro);
                Productos = await productos.ObtenerLista("");
                CodMov = await lineas.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
