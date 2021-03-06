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
    public class VentasResumenModel : PageModel
    {
        private readonly ICrudApi<VentasResumenViewModel, int> service;
        private readonly ICrudApi<CajasViewModel, string> cajas;
        private readonly ICrudApi<CajerosViewModel, string> cajeros;
        private readonly ICrudApi<ClientesViewModel, string> clientes;

        private readonly ICrudApi<VendedorViewModel, string> vendedor;
        private readonly ICrudApi<ProductosViewModel, string> productos;

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public VentasResumenViewModel[] EncVtas { get; set; }
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

        public VentasResumenModel(ICrudApi<CajasViewModel, string> cajas, ICrudApi<ClientesViewModel, string> clientes, ICrudApi<CajerosViewModel, string> cajeros, ICrudApi<VentasResumenViewModel, int> service, ICrudApi<ProductosViewModel, string> productos, ICrudApi<VendedorViewModel, string> vendedor)
        {
            this.service = service;
            this.cajas = cajas;
            this.cajeros = cajeros;
            this.vendedor = vendedor;
            this.productos = productos;
            this.clientes = clientes;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DateTime time = new DateTime();
                
                if (filtro.FechaFinal.Date == time.Date)
                {
                    filtro.FechaInicial = DateTime.Now.AddMonths(-1);
                    filtro.FechaFinal = DateTime.Now;
                }

                if (PageContext.HttpContext.Request.Query.Count > 0)
                {
                    EncVtas = await service.ObtenerListaVentasResumen(filtro);
                }
                else
                {
                    EncVtas = new VentasResumenViewModel[0];
                }

                
                Cajas = await cajas.ObtenerLista("");
                Cajeros = await cajeros.ObtenerLista("");
                Vendedores = await vendedor.ObtenerLista("");
                Productos = await productos.ObtenerLista("");
                Clientes = await clientes.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
