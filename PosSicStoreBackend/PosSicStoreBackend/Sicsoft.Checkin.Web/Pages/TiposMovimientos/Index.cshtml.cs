using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.TiposMovimientos
{
    public class IndexModel : PageModel
    {
        private readonly ICrudApi<TiposMovimientosViewModel, string> service;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public TiposMovimientosViewModel[] Tipos { get; set; }

        public IndexModel(ICrudApi<TiposMovimientosViewModel, string> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Tipos = await service.ObtenerLista(filtro);
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> OnGetEliminar(string id)
        {
            try
            {

                await service.Eliminar(id);
                return new JsonResult(true);
            }
            catch (ApiException ex)
            {
                return new JsonResult(false);
            }
        }
    }
}
