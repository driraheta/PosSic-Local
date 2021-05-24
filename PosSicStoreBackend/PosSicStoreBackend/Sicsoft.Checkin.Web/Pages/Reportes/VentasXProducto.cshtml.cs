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
    public class VentasXProductoModel : PageModel
    {
        private readonly ICrudApi<VentasXProductoViewModel, int> service;
        private readonly ICrudApi<VentasProductosFacturas, int> service2;
        private readonly ICrudApi<CajasViewModel, string> cajas;
        private readonly ICrudApi<CajerosViewModel, string> cajeros;
        private readonly ICrudApi<ClientesViewModel, string> clientes;

        private readonly ICrudApi<VendedorViewModel, string> vendedor;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        private readonly ICrudApi<LineasViewModel, string> lineas;

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public VentasXProductoViewModel[] EncVtas { get; set; }

        [BindProperty]
        public VentasProductosFacturas[] EncVtas2 { get; set; }

        [BindProperty]
        public VentasXProductoViewModel[][] EncVtasMatriz { get; set; }


        [BindProperty]
        public CajasViewModel[] Cajas { get; set; }

        [BindProperty]
        public CajerosViewModel[] Cajeros { get; set; }


        [BindProperty]
        public VendedorViewModel[] Vendedores { get; set; }

        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }
        [BindProperty]
        public ClientesViewModel[] Clientes { get; set; }

        [BindProperty]
        public LineasViewModel[] Lineas { get; set; }

        public VentasXProductoModel(ICrudApi<LineasViewModel, string> lineas, ICrudApi<CajasViewModel, string> cajas, ICrudApi<ClientesViewModel, string> clientes, ICrudApi<CajerosViewModel, string> cajeros, ICrudApi<VentasXProductoViewModel, int> service, ICrudApi<ProductosViewModel, string> productos, ICrudApi<VendedorViewModel, string> vendedor, ICrudApi<VentasProductosFacturas, int> service2)
        {
            this.service = service;
            this.cajas = cajas;
            this.cajeros = cajeros;
            this.vendedor = vendedor;
            this.productos = productos;
            this.clientes = clientes;
            this.lineas = lineas;
            this.service2 = service2;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DateTime time = new DateTime();

                if (filtro.FechaFinal.Date == time.Date)
                {
                    filtro.FechaInicial = DateTime.Now.AddMonths(-6);
                    filtro.FechaFinal = DateTime.Now;
                }
                filtro.FechaFinal = filtro.FechaFinal.AddDays(1);
                EncVtas = await service.ObtenerListaVentasXProducto(filtro);
                EncVtas2 = await service2.ObtenerListaVentasXProductoFactura(filtro);
                var EncVtas1 = EncVtas.GroupBy(a => a.CodLinea);
                

                

                Cajas = await cajas.ObtenerLista("");
                Cajeros = await cajeros.ObtenerLista("");
                Vendedores = await vendedor.ObtenerLista("");
                Productos = await productos.ObtenerLista("");
                Clientes = await clientes.ObtenerLista("");
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
