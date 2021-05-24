using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Models.Inventario;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.MovimientoInventarios
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<EncMovInvViewModel, string> service;
        private readonly ICrudApi<MovimientoInventariosViewModel, string> service2;
        private readonly ICrudApi<TiposMovimientosViewModel, string> tipos;
        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<ProductosViewModel, string> productos;

        [BindProperty]
        public EncMovInvViewModel[] Objeto { get; set; }
        [BindProperty]
        public TiposMovimientosViewModel[] Tipos { get; set; }
        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        public IndexModel(ICrudApi<EncMovInvViewModel, string> service, ICrudApi<TiposMovimientosViewModel, string> tipos,
            ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<ProductosViewModel, string> productos, ICrudApi<MovimientoInventariosViewModel, string> service2)
        {
            this.service = service;
            this.service2 = service2;
            this.tipos = tipos;
            this.proveedores = proveedores;
            this.productos = productos;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                
                Objeto = await service.ObtenerLista(filtro);
                Tipos = await tipos.ObtenerLista("");

                return Page();
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }
        public async Task<IActionResult> OnGetEliminar(string id, string id2)
        {
            try
            {
                var ids = Convert.ToInt32(id2);
                await service2.EliminarInventario(id, ids);
               
                return new JsonResult(true);
            }
            catch (ApiException ex)
            {
                return new JsonResult(false);
            }
        }


    }
}
