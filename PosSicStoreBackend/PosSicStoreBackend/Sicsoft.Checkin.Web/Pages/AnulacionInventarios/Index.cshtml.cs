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

namespace Sicsoft.Checkin.Web.Pages.AnulacionInventarios
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<MovimientoInventariosViewModel, string> service;
        private readonly ICrudApi<TiposMovimientosViewModel, string> tipos;
        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        private readonly ICrudApi<EncMovInvViewModel, string> enc;
        private readonly ICrudApi<DetMovInvViewModel, string> det;

        [BindProperty]
        public MovimientoInventariosViewModel Objeto { get; set; }
        [BindProperty]
        public TiposMovimientosViewModel[] Tipos { get; set; }
        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public IndexModel(ICrudApi<MovimientoInventariosViewModel, string> service, ICrudApi<TiposMovimientosViewModel, string> tipos,
        ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<ProductosViewModel, string> productos, ICrudApi<EncMovInvViewModel, string> enc,
        ICrudApi<DetMovInvViewModel, string> det)
        {
            this.service = service;
            this.tipos = tipos;
            this.proveedores = proveedores;
            this.productos = productos;
            this.enc = enc;
            this.det = det;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Tipos = await tipos.ObtenerLista("");
                Tipos = Tipos.Where(a => a.Modifica == true).ToArray();


                Proveedores = await proveedores.ObtenerLista("");
                Productos = await productos.ObtenerLista("");

                return Page();
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }

        public async Task<IActionResult> OnGetMovimientos(string id)
        {
            try
            {
                ParametrosFiltro filtro = new ParametrosFiltro();
                filtro.Codigo = id;
                var Movimientos = await enc.ObtenerLista(filtro);
               
                return new JsonResult(Movimientos);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(error);
            }
        }

        public async Task<IActionResult> OnGetMovimientosDetalle(string id, string codMov)
        {
            try
            {
                ParametrosFiltro filtro = new ParametrosFiltro();
                var Movimientos = await enc.ObtenerPorIdEncabezado(id, codMov);
                filtro.Codigo = codMov;
                filtro.Cod = Convert.ToInt32(id);
                var detalle = await det.ObtenerLista(filtro);

                var resp = new
                {
                    Movimientos,
                    detalle
                };

                return new JsonResult(resp);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(error);
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                await service.EliminarInventario(Objeto.EncMovInv.CodMov, Objeto.EncMovInv.NumDocto);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }

    }
}
