using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ICrudApi<ClientesViewModel, string> service;
        private readonly ICrudApi<TiposPreciosViewModel, int> serviceTiposPrecios;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public ClientesViewModel[] Clientes { get; set; }
        [BindProperty]
        public TiposPreciosViewModel[] TiposPrecios { get; set; }

        public IndexModel(ICrudApi<ClientesViewModel, string> service, ICrudApi<TiposPreciosViewModel, int> serviceTiposPrecios)
        {
            this.service = service;
            this.serviceTiposPrecios = serviceTiposPrecios;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Clientes = await service.ObtenerLista(filtro);
                TiposPrecios = await serviceTiposPrecios.ObtenerLista("");

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
                id = id.Replace( "_", "?").Replace( "[","¿").Replace( "#", "/");
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
