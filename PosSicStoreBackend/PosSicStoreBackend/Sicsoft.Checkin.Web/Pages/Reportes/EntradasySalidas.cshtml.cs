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
    public class EntradasySalidasModel : PageModel
    {
        private readonly ICrudApi<ReportesProductosViewModel, string> service;
        private readonly ICrudApi<LineasViewModel, string> lineas;

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        [BindProperty]
        public ReportesProductosViewModel[] EncVtas { get; set; }


        [BindProperty]
        public LineasViewModel[] Lineas { get; set; }

        public EntradasySalidasModel(ICrudApi<ReportesProductosViewModel, string> service, ICrudApi<LineasViewModel, string> lineas)
        {
            this.service = service;
            this.lineas = lineas;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (PageContext.HttpContext.Request.Query.Count > 0)
                {
                    EncVtas = await service.ObtenerEntradasySalidas(filtro);
                }
                else
                {
                    EncVtas = new ReportesProductosViewModel[0];
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
