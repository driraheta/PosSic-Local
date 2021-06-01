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
    public class ProductosModel : PageModel
    {
        private readonly ICrudApi<ProductosViewModel, string> service;
        private readonly ICrudApi<ProveedoresViewModel, string> serviceP;
        private readonly ICrudApi<LineasViewModel, string> lineas;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }

        [BindProperty]
        public LineasViewModel[] Lineas { get; set; }
        [BindProperty]
        public ProductosViewModel[] Producto { get; set; }

        public ProductosModel(ICrudApi<ProductosViewModel, string> service, ICrudApi<ProveedoresViewModel, string> serviceP, ICrudApi<LineasViewModel, string> lineas)
        {
            this.service = service;
            this.serviceP = serviceP;
            this.lineas = lineas;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {                
                Proveedores = await serviceP.ObtenerLista("");
                if (PageContext.HttpContext.Request.Query.Count > 0)
                {
                    Producto = await service.ObtenerLista(filtro);
                }
                else
                {
                    Producto = new ProductosViewModel[0];
                }
                
                Lineas = await lineas.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
