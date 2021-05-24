using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.Productos
{
    public class NuevoModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<ProductosViewModel, string> service;
        private readonly ICrudApi<ProveedoresViewModel, string> serviceProveedor;
        private readonly ICrudApi<LineasViewModel, string> serviceLineas;
        private readonly ICrudApi<CabysViewModel, string> serviceCabys;
        private readonly ICrudApi<CodigosTarifaImpuestosViewModel, string> serviceCodigos;
        private readonly ICrudApi<ParametrosViewModel, string> serviceParametros;
        [BindProperty]
        public ProductosViewModel Producto { get; set; }

        [BindProperty]
        public ParametrosViewModel[] Parametros { get; set; }

        [BindProperty]
        public LineasViewModel[] Lineas { get; set; }

        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }

        [BindProperty]
        public CabysViewModel[] Cabys { get; set; }

        [BindProperty]
        public CodigosTarifaImpuestosViewModel[] Codigos { get; set; }

        public NuevoModel(ICrudApi<ParametrosViewModel, string> serviceParametros,ICrudApi<ProductosViewModel, string> service, ICrudApi<ProveedoresViewModel, string> serviceProveedor, ICrudApi<CabysViewModel, string> serviceCabys, ICrudApi<LineasViewModel, string> serviceLineas, ICrudApi<CodigosTarifaImpuestosViewModel, string> serviceCodigos)
        {
            this.service = service;
            this.serviceProveedor = serviceProveedor;
            this.serviceLineas = serviceLineas;
            this.serviceCabys = serviceCabys;
            this.serviceCodigos = serviceCodigos;
            this.serviceParametros = serviceParametros;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Lineas = await serviceLineas.ObtenerLista("");
                Proveedores = await serviceProveedor.ObtenerLista("");
                //Cabys = await serviceCabys.ObtenerLista("");
                Codigos = await serviceCodigos.ObtenerLista("");
                Parametros = await serviceParametros.ObtenerLista("");
                return Page();

            }
            catch (ApiException ex)
            {

                throw ex;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Producto.ImagenBase64 = Producto.Upload.ConvertirBase64();
                await service.Agregar(Producto);
                return RedirectToPage("./Index");
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }

        public async Task<IActionResult> OnGetCabys(string id)
        {
            try
            {
                ParametrosFiltro p = new ParametrosFiltro();
                p.Texto = id;
                var sp = await serviceCabys.ObtenerLista(p);

                return new JsonResult(sp);
            }
            catch (ApiException ex)
            {

                return new JsonResult(ex);
            }
        }


        //public async Task OnPostCargarImagenesAsync()
        //{
        //    var directory = @$"{ Path.GetTempPath()}\{Id}";
        //    Directory.CreateDirectory(directory);

        //    foreach (var formFile in HttpContext.Request.Form.Files)
        //    {
        //        var filePath = Path.Combine(directory, Path.GetRandomFileName());

        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            await formFile.CopyToAsync(stream);
        //        }
        //    }

        //}
    }
}
