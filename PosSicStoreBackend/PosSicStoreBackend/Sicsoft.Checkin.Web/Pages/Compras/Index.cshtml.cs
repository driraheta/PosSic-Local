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
using Sicsoft.Checkin.Web.Models.Compras;

namespace Sicsoft.Checkin.Web.Pages.Compras
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<EncCompras, string> service;
        private readonly ICrudApi<ComprasR, string> service2;

        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }
        [BindProperty]
        public EncCompras[] Objeto { get; set; }
    
        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public IndexModel(ICrudApi<EncCompras, string> service , ICrudApi<ComprasR, string> service2,
           ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<ProductosViewModel, string> productos)
        {
            this.service = service;
            this.service2 = service2;
            this.proveedores = proveedores;
            this.productos = productos;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {

                Objeto = await service.ObtenerLista(filtro);
                Proveedores = await proveedores.ObtenerLista("");
                return Page();
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }

        public async Task<IActionResult> OnGetProducto(string id)
        {
            try
            {
                var producto = await productos.ObtenerPorId(id);


                return new JsonResult(producto);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(error);
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
