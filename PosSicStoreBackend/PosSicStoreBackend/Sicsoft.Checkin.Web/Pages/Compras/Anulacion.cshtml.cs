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
    public class AnulacionModel : PageModel
    {
        private readonly IConfiguration configuration;

        private readonly ICrudApi<ComprasR, string> service;

        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<EncCompras, string> enc;
        private readonly ICrudApi<DetCompras, string> det;

        [BindProperty]
        public ComprasR Objeto { get; set; }

        [BindProperty]
        public string  ID { get; set; }

        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public AnulacionModel(ICrudApi<ComprasR, string> service, ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<EncCompras, string> enc,
             ICrudApi<DetCompras, string> det
            )
        {
            this.service = service;
            this.proveedores = proveedores;
            this.enc = enc;
            this.det = det;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
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

                await service.EliminarInventario(Objeto.EncCompras.CodProveedor, Objeto.EncCompras.NumCompra);
                return RedirectToPage("./Anulacion");
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
