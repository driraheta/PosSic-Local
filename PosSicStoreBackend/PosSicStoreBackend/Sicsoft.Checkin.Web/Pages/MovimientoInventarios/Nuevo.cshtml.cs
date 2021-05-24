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
    public class NuevoModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<MovimientoInventariosViewModel, string> service;
        private readonly ICrudApi<TiposMovimientosViewModel, string> tipos;
        private readonly ICrudApi<ProveedoresViewModel, string> proveedores;
        private readonly ICrudApi<ProductosViewModel, string> productos;

        [BindProperty]
        public MovimientoInventariosViewModel Objeto { get; set; }
        [BindProperty]
        public TiposMovimientosViewModel[] Tipos { get; set; }
        [BindProperty]
        public ProveedoresViewModel[] Proveedores { get; set; }
        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public NuevoModel(ICrudApi<MovimientoInventariosViewModel, string> service, ICrudApi<TiposMovimientosViewModel, string> tipos,
            ICrudApi<ProveedoresViewModel, string> proveedores, ICrudApi<ProductosViewModel, string> productos)
        {
            this.service = service;
            this.tipos = tipos;
            this.proveedores = proveedores;
            this.productos = productos;
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

                Recibido recibido = JsonConvert.DeserializeObject<Recibido>(recibidos);
                var tipo = await tipos.ObtenerPorId(recibido.encMovInv.CodMov);
                Objeto = new MovimientoInventariosViewModel();
                Objeto.EncMovInv = new EncMovInvViewModel();
                Objeto.detMovInv = new DetMovInvViewModel[recibido.productos.Length];
                Objeto.HistoInv = new HistInvViewModel[recibido.productos.Length];

                int numdoc = tipo.NumDoctoSig + 1;
                Objeto.EncMovInv.NumDocto = numdoc;
                Objeto.EncMovInv.CodMov = recibido.encMovInv.CodMov;
                Objeto.EncMovInv.Detalle = recibido.encMovInv.Detalle;
                Objeto.EncMovInv.FecDocto = recibido.encMovInv.FecDocto.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                short cantidad = 1;

                foreach (var item in recibido.productos)
                {
                    Objeto.detMovInv[cantidad - 1] = new DetMovInvViewModel();
                    Objeto.detMovInv[cantidad - 1].NumDocto = Objeto.EncMovInv.NumDocto;
                    Objeto.detMovInv[cantidad - 1].CodMov = Objeto.EncMovInv.CodMov;
                    Objeto.detMovInv[cantidad - 1].NumLinea = cantidad;
                    Objeto.detMovInv[cantidad - 1].CodPro = item.codPro;
                    Objeto.detMovInv[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.detMovInv[cantidad - 1].CostoPro = item.PreVta;

                    Objeto.HistoInv[cantidad - 1] = new HistInvViewModel();
                    Objeto.HistoInv[cantidad - 1].CodPro = item.codPro;
                    Objeto.HistoInv[cantidad - 1].Fecha = Objeto.EncMovInv.FecDocto;
                    Objeto.HistoInv[cantidad - 1].CodMov = recibido.encMovInv.CodMov;
                    Objeto.HistoInv[cantidad - 1].NumDocto = numdoc.ToString();
                    Objeto.HistoInv[cantidad - 1].Descripcion = recibido.encMovInv.Detalle;
                    Objeto.HistoInv[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.HistoInv[cantidad - 1].Costo = item.PreVta;
                    Objeto.HistoInv[cantidad - 1].NumLinea = cantidad;
                    Objeto.HistoInv[cantidad - 1].CodProveedor = recibido.encMovInv.CodProveedor;



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



        public async Task<IActionResult> OnPostGenerar(string recibidos)
        {
            try
            {

                Recibido recibido = JsonConvert.DeserializeObject<Recibido>(recibidos);
                var tipo = await tipos.ObtenerPorId(recibido.encMovInv.CodMov);
                Objeto = new MovimientoInventariosViewModel();
                Objeto.EncMovInv = new EncMovInvViewModel();
                Objeto.detMovInv = new DetMovInvViewModel[recibido.productos.Length];
                Objeto.HistoInv = new HistInvViewModel[recibido.productos.Length];

                int numdoc = tipo.NumDoctoSig + 1;
                Objeto.EncMovInv.NumDocto = numdoc;
                Objeto.EncMovInv.CodMov = recibido.encMovInv.CodMov;
                Objeto.EncMovInv.Detalle = recibido.encMovInv.Detalle;
                Objeto.EncMovInv.FecDocto = recibido.encMovInv.FecDocto.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                short cantidad = 1;

                foreach (var item in recibido.productos)
                {
                    Objeto.detMovInv[cantidad - 1] = new DetMovInvViewModel();
                    Objeto.detMovInv[cantidad - 1].NumDocto = Objeto.EncMovInv.NumDocto;
                    Objeto.detMovInv[cantidad - 1].CodMov = Objeto.EncMovInv.CodMov;
                    Objeto.detMovInv[cantidad - 1].NumLinea = cantidad;
                    Objeto.detMovInv[cantidad - 1].CodPro = item.codPro;
                    Objeto.detMovInv[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.detMovInv[cantidad - 1].CostoPro = item.PreVta;

                    Objeto.HistoInv[cantidad - 1] = new HistInvViewModel();
                    Objeto.HistoInv[cantidad - 1].CodPro = item.codPro;
                    Objeto.HistoInv[cantidad - 1].Fecha = Objeto.EncMovInv.FecDocto;
                    Objeto.HistoInv[cantidad - 1].CodMov = recibido.encMovInv.CodMov;
                    Objeto.HistoInv[cantidad - 1].NumDocto = numdoc.ToString();
                    Objeto.HistoInv[cantidad - 1].Descripcion = recibido.encMovInv.Detalle;
                    Objeto.HistoInv[cantidad - 1].Cantidad = item.cantidad;
                    Objeto.HistoInv[cantidad - 1].Costo = item.PreVta;
                    Objeto.HistoInv[cantidad - 1].NumLinea = cantidad;
                    Objeto.HistoInv[cantidad - 1].CodProveedor = recibido.encMovInv.CodProveedor;



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
    }
}
