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

namespace Sicsoft.Checkin.Web.Pages.Reportes
{
    public class KardexModel : PageModel
    {
        private readonly ICrudApi<KardexViewModel, string> service;
        private readonly ICrudApi<ProductosViewModel, string> prods;
        private readonly ICrudApi<TiposMovimientosViewModel, string> mov;

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public KardexViewModel[] EncVtas { get; set; }


        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }



        [BindProperty]
        public TiposMovimientosViewModel[] Movimientos { get; set; }

        public KardexModel(ICrudApi<KardexViewModel, string> service, ICrudApi<ProductosViewModel, string> prods, ICrudApi<TiposMovimientosViewModel, string> mov)
        {
            this.service = service;
            this.prods = prods;
            this.mov = mov;
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
                
                if (PageContext.HttpContext.Request.Query.Count > 0)
                {
                    EncVtas = await service.ObtenerKardez(filtro);
                }
                else
                {
                    EncVtas = new KardexViewModel[0];
                }
                
                Productos = await prods.ObtenerLista("");
                Movimientos = await mov.ObtenerLista("");

                if (!string.IsNullOrEmpty(filtro.Texto) && filtro.Texto == "NULL" && filtro.FechaFinal.Date != time.Date)
                {
                    throw new Exception("Debe digitar el producto");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "");
                }

                return Page();
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, ex.Message);

                return Page();
            }
        }
    }
}
