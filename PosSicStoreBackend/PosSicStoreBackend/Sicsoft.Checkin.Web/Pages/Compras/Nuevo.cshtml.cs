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
    public class NuevoModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<ComprasR, string> service;

        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<ProductosViewModel, string> productos;

        [BindProperty]
        public ComprasR Objeto { get; set; }

        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public NuevoModel(ICrudApi<ComprasR, string> service,
           ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<ProductosViewModel, string> productos)
        {
            this.service = service;

            this.proveedores = proveedores;
            this.productos = productos;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {



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


        public async Task<IActionResult> OnGetPostAsync(string recibidos)
        {
            try
            {

                RecibidoC recibido = JsonConvert.DeserializeObject<RecibidoC>(recibidos);

                Objeto = new ComprasR();
                Objeto.EncCompras = new EncCompras();
                Objeto.DetCompras = new DetCompras[recibido.DetCompras.Length];


                Objeto.EncCompras.NumCompra = recibido.EncCompras.NumCompra;

                Objeto.EncCompras.CodProveedor = recibido.EncCompras.CodProveedor;
                Objeto.EncCompras.FechaCompra = recibido.EncCompras.FechaCompra;
                Objeto.EncCompras.FechaVencimiento = recibido.EncCompras.FechaVencimiento;
                Objeto.EncCompras.SubTotal = recibido.EncCompras.SubTotal;
                Objeto.EncCompras.TotalDescuento = recibido.EncCompras.TotalDescuento;
                Objeto.EncCompras.ImptoVta = recibido.EncCompras.ImptoVta;
                Objeto.EncCompras.TotalCompra = recibido.EncCompras.TotalCompra;
                short cantidad = 1;

                foreach (var item in recibido.DetCompras)
                {
                    Objeto.DetCompras[cantidad - 1] = new DetCompras();
                    Objeto.DetCompras[cantidad - 1].NumCompra = Objeto.EncCompras.NumCompra;
                    Objeto.DetCompras[cantidad - 1].CodProveedor = Objeto.EncCompras.CodProveedor;
                    Objeto.DetCompras[cantidad - 1].NumLinea = cantidad;
                    Objeto.DetCompras[cantidad - 1].CodPro = item.codPro;
                    Objeto.DetCompras[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.DetCompras[cantidad - 1].CostoPro = item.PreVta;
                    Objeto.DetCompras[cantidad - 1].ImptoVta = item.impuesto;
                    Objeto.DetCompras[cantidad - 1].PorDescuento = item.descuento;





                    cantidad++;
                }

                await service.Agregar(Objeto);
                return new JsonResult(true);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(false);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

                return new JsonResult(false);
            }
        }




        //Esto es una prueba de metodo post 
     
        public async Task<IActionResult> OnPostGenerar(  string recibidos)
        {
            try
            {

                RecibidoC recibido = JsonConvert.DeserializeObject<RecibidoC>(recibidos);

                Objeto = new ComprasR();
                Objeto.EncCompras = new EncCompras();
                Objeto.DetCompras = new DetCompras[recibido.DetCompras.Length];


                Objeto.EncCompras.NumCompra = recibido.EncCompras.NumCompra;

                Objeto.EncCompras.CodProveedor = recibido.EncCompras.CodProveedor;
                Objeto.EncCompras.FechaCompra = recibido.EncCompras.FechaCompra.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                Objeto.EncCompras.FechaVencimiento = recibido.EncCompras.FechaVencimiento;
                Objeto.EncCompras.SubTotal = recibido.EncCompras.SubTotal;
                Objeto.EncCompras.TotalDescuento = recibido.EncCompras.TotalDescuento;
                Objeto.EncCompras.ImptoVta = recibido.EncCompras.ImptoVta;
                Objeto.EncCompras.TotalCompra = recibido.EncCompras.TotalCompra;
                short cantidad = 1;

                foreach (var item in recibido.DetCompras)
                {
                    Objeto.DetCompras[cantidad - 1] = new DetCompras();
                    Objeto.DetCompras[cantidad - 1].NumCompra = Objeto.EncCompras.NumCompra;
                    Objeto.DetCompras[cantidad - 1].CodProveedor = Objeto.EncCompras.CodProveedor;
                    Objeto.DetCompras[cantidad - 1].NumLinea = cantidad;
                    Objeto.DetCompras[cantidad - 1].CodPro = item.codPro;
                    Objeto.DetCompras[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.DetCompras[cantidad - 1].CostoPro = item.PreVta;
                    Objeto.DetCompras[cantidad - 1].ImptoVta = item.impuesto;
                    Objeto.DetCompras[cantidad - 1].PorDescuento = item.descuento;





                    cantidad++;
                }

                await service.Agregar(Objeto);
                return new JsonResult(true);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(false);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

                return new JsonResult(false);
            }
        }


        public async Task<IActionResult> OnGetProveedor(string id)
        {
            try
            {
                var proveedor = await proveedores.ObtenerPorId(id);
                return new JsonResult(proveedor);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(error);
            }
        }

    }
}
