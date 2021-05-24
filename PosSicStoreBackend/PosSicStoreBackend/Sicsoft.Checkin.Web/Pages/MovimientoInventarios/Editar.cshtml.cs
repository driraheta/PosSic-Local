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
    public class EditarModel : PageModel
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
        public int cantidad { get; set; }

        [BindProperty]
        public decimal costo { get; set; }
        [BindProperty]
        public TiposMovimientosViewModel[] Tipos { get; set; }
        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        [BindProperty]
        public EncMovInvViewModel Encabezado { get; set; }

        [BindProperty]
        public DetMovInvViewModel[] Detalle { get; set; }

        public EditarModel(ICrudApi<MovimientoInventariosViewModel, string> service, ICrudApi<TiposMovimientosViewModel, string> tipos,
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


        public async Task<IActionResult> OnGetAsync(string id, string codMov)
        {
            try
            {
                ParametrosFiltro filtro = new ParametrosFiltro();
                 Encabezado = await enc.ObtenerPorIdEncabezado(id, codMov);
                Tipos = await tipos.ObtenerLista("");
                Productos = await productos.ObtenerLista("");
                filtro.Codigo = codMov;
                filtro.Cod = Convert.ToInt32(id);
                Detalle = await det.ObtenerLista(filtro);
                cantidad = 0;
                costo = 0;
                foreach(var item in Detalle)
                {
                    cantidad += item.Cantidad;
                    costo += item.CostoPro;
                }


                return Page();
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
