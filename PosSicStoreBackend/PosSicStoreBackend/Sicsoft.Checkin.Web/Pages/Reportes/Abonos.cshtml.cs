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
    public class AbonosModel : PageModel
    {
        private readonly ICrudApi<AbonosViewModel, string> service;
        private readonly ICrudApi<EncApartadosViewModel, int> apartados;
        private readonly ICrudApi<ClientesViewModel, string> clientes;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public AbonosViewModel[] Abonos { get; set; }
        [BindProperty]
        public ClientesViewModel[] Clientes { get; set; }

        [BindProperty]
        public EncApartadosViewModel[] Apartados { get; set; }

        public AbonosModel(ICrudApi<AbonosViewModel, string> service, ICrudApi<ClientesViewModel, string> clientes, ICrudApi<EncApartadosViewModel, int> apartados)
        {
            this.service = service;
            this.clientes = clientes;
            this.apartados = apartados;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DateTime time = new DateTime();

                if(filtro.FechaFinal.Date == time.Date)
                {

                
                filtro.FechaInicial = DateTime.Now.AddMonths(-1);
                filtro.FechaFinal = DateTime.Now;
                }
                Abonos = await service.ObtenerLista(filtro);
                Clientes = await clientes.ObtenerLista("");
                Apartados = await apartados.ObtenerLista("");
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
