using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Models.CierreCaja;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.CierreCaja
{
    public class IndexModel : PageModel
    {
        private readonly ICrudApi<CierreCajaViewModel, string> service;
        private readonly ICrudApi<CajasViewModel, string> cajas;
        private readonly ICrudApi<CajerosViewModel, string> cajeros;
        private readonly ICrudApi<ParametrosViewModel, string> param;
        private readonly ICrudApi<EncVtasViewModel, int> ventas;


        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public CierreCajaViewModel[] Cierre { get; set; }


        [BindProperty]
        public CierreCajaViewModel CierreModel { get; set; }

        [BindProperty]
        public CajasViewModel[] Cajas { get; set; }

        [BindProperty]
        public CajerosViewModel[] Cajeros { get; set; }
        [BindProperty]
        public ParametrosViewModel[] Parametros { get; set; }

        [BindProperty]
        public EncVtasViewModel[] EncVtas { get; set; }

        public IndexModel(ICrudApi<CierreCajaViewModel, string> service, ICrudApi<EncVtasViewModel, int> ventas, ICrudApi<CajasViewModel, string> cajas, ICrudApi<CajerosViewModel, string> cajeros, ICrudApi<ParametrosViewModel, string> param)
        {
            this.service = service;
            this.cajas = cajas;
            this.cajeros = cajeros;
            this.param = param;
            this.ventas = ventas;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Cajas = await cajas.ObtenerLista("");
                Cajeros = await cajeros.ObtenerLista("");
                Cierre = await service.ObtenerLista("");
                Parametros = await param.ObtenerLista("");
               

                var Fecha = Parametros.FirstOrDefault().FechaSistema;
                var Fecha2 = Fecha.AddDays(1);
                Cierre = Cierre.Where(a => a.Abierta == true && a.FecCaja >= Fecha && a.FecCaja <= Fecha2 ).ToArray();

                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> OnGetCajero(string id)
        {
            try
            {
                if(id != "NULL")
                {

                ParametrosFiltro p = new ParametrosFiltro();
                p.Codigo = id;
                var sp = await service.ObtenerLista(p);
                Parametros = await param.ObtenerLista("");

                var Fecha = Parametros.FirstOrDefault().FechaSistema;
                var Fecha2 = Fecha.AddDays(1);

                    p.Codigo = "NULL";
                    p.FechaInicial = Fecha;
                EncVtas = await ventas.ObtenerLista(p);


                sp = sp.Where(a => a.Abierta == true && a.FecCaja >= Fecha && a.FecCaja <= Fecha2).ToArray();

                var Cierre = sp.FirstOrDefault();
                var ventas1 = EncVtas.Where(a => a.FecFactura >= Fecha && a.FecFactura <= Fecha2 && a.CodCaja == Cierre.CodCaja && a.CodCajero == Cierre.CodCajero && a.Apdo == false && a.CondicionVenta == 1).ToList();
                    var ventas2 = EncVtas.Where(a => a.FecFactura >= Fecha && a.FecFactura <= Fecha2 && a.CodCaja == Cierre.CodCaja && a.CodCajero == Cierre.CodCajero && a.Apdo == true && a.CondicionVenta == 1).ToList();


                    // List<SegundoSelectVentas> segundoSelect = new List<SegundoSelectVentas>();


                    if (Cierre.Activa == true)
                    {
                        throw new Exception("Caja se encuentra activa");
                    }


                    Cierre.Efectivo = ventas1.Sum(a => a.Total - a.MtoTarjeta - a.MtoPago - a.MtoCheque ) - ventas1.Sum(a => a.MtoDol * a.TipoCambio); /*- ventas1.Sum(a => a.MtoTarjeta) - ventas1.Sum(a => a.MtoPago);*/
                Cierre.Dolares = ventas1.Sum(a => a.MtoDol);
                Cierre.Tarjetas = ventas1.Sum(a => a.MtoTarjeta);
                Cierre.OtrosPagos = ventas1.Sum(a => a.MtoPago);
                    Cierre.MtoVendido = ventas1.Sum(a => a.Total); //+ ventas1.Sum(a => a.MtoDol * a.TipoCambio);
                    Cierre.TipoCambio = (ventas1.FirstOrDefault() == null ? 0 : ventas1.FirstOrDefault().TipoCambio);
                    Cajeros = await cajeros.ObtenerLista("");
                    Cierre.CortesDescripcion = Cajeros.Where(a => a.CodCajero == Cierre.CodCajero).FirstOrDefault().NomCajero;

                    foreach (var item in ventas2)
                    {
                        foreach (var item2 in item.Detalle)
                        {
                            SegundoSelectVentas se = new SegundoSelectVentas();
                            se.Total = item2.PrecioVenta * item2.Cantidad;
                            Cierre.MtoVendido += se.Total;
                            Cierre.Efectivo += Convert.ToDecimal(se.Total - item.MtoTarjeta - item.MtoPago);
                            Cierre.Tarjetas += Convert.ToDecimal(item.MtoTarjeta);
                            Cierre.OtrosPagos += Convert.ToDecimal(item.MtoPago);
                           // segundoSelect.Add(se);
                        }
                    }



                

                return new JsonResult(Cierre);
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            //catch (ApiException ex)
            //{

            //    return new JsonResult(ex.Message);
            //}
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                CierreModel.CodSupervisor = User.Identity.Name;

                await service.Agregar(CierreModel);
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
