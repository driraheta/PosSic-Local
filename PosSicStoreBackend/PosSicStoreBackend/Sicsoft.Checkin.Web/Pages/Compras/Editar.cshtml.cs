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
    public class EditarModel : PageModel
    {
        private readonly IConfiguration configuration;

        private readonly ICrudApi<ComprasR, string> service;

        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<EncCompras, string> enc;
        private readonly ICrudApi<DetCompras, string> det;
        private readonly ICrudApi<ProductosViewModel, string> prod;

        [BindProperty]
        public ComprasR Objeto { get; set; }

        [BindProperty]
        public string ID { get; set; }


        [BindProperty]
        public EncCompras Encabezado { get; set; }

        [BindProperty]
        public DetCompras[] Detalle { get; set; }

        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }
        public EditarModel(ICrudApi<ComprasR, string> service, ICrudApi<ProductosViewModel, string> prod, ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<EncCompras, string> enc,
             ICrudApi<DetCompras, string> det
            )
        {
            this.service = service;
            this.proveedores = proveedores;
            this.enc = enc;
            this.det = det;
            this.prod = prod;
        }
        public async Task<IActionResult> OnGetAsync(string id, string codProv)
        {
            try
            {
                ParametrosFiltro filtro = new ParametrosFiltro();
                Proveedores = await proveedores.ObtenerLista("");
                Productos = await prod.ObtenerLista("");
                Encabezado = await enc.ObtenerPorIdEncabezado(id, codProv);
                filtro.Codigo = codProv;
                filtro.Cod = Convert.ToInt32(id);
                Detalle = await det.ObtenerLista(filtro);



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
