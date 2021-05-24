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
    public class LineasModel : PageModel
    {
        private readonly ICrudApi<LineasViewModel, string> service;
        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public LineasViewModel[] Lineas { get; set; }

        public LineasModel(ICrudApi<LineasViewModel, string> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Lineas = await service.ObtenerLista(filtro);
                return Page();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      
    }
}
