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
    public class ApartadosModel : PageModel
    {
        private readonly ICrudApi<EncApartadosViewModel, int> service;
        private readonly ICrudApi<ClientesViewModel, string> clientes;
        private readonly ICrudApi<VendedorViewModel, string> vendedor;
        private readonly ICrudApi<ProductosViewModel, string> productos;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public EncApartadosViewModel[] Apartados { get; set; }
        [BindProperty]
        public ClientesViewModel[] Clientes { get; set; }

        [BindProperty]
        public VendedorViewModel[] Vendedores { get; set; }

        [BindProperty]
        public ProductosViewModel[] Productos { get; set; }

        public ApartadosModel(ICrudApi<EncApartadosViewModel, int> service, ICrudApi<ProductosViewModel, string> productos, ICrudApi<ClientesViewModel, string> clientes, ICrudApi<VendedorViewModel, string> vendedor)
        {
            this.service = service;
            this.clientes = clientes;
            this.vendedor = vendedor;
            this.productos = productos;
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
                    Apartados = await service.ObtenerLista(filtro);
                }
                else
                {
                    Apartados = new EncApartadosViewModel[0];
                }
                
                Clientes = await clientes.ObtenerLista("");
                Vendedores = await vendedor.ObtenerLista("");
                Productos = await productos.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
