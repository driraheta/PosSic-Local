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
    public class ComprasModel : PageModel
    {
        private readonly ICrudApi<ComprasReportes, int> service;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public ComprasReportes[] EncVtas { get; set; }

        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public ComprasModel(ICrudApi<ComprasReportes, int> service, ICrudApi<ProductosViewModel, string> productos)
        {
            this.service = service;
            this.productos = productos;
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

                EncVtas = await service.ObtenerCompras(filtro);
                Productos = await productos.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
