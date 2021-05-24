using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Models.Roles;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web.Pages.SeguridadRolesModulos
{
    [AllowAnonymous]
    [DisableRequestSizeLimit]
    [RequestSizeLimit(long.MaxValue)]
    
    public class RolesModulosModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ICrudApi<SeguridadRoles, int> roles;
        private readonly ICrudApi<SeguridadModulos, int> modulos;
        private readonly ICrudApi<SeguridadRolesModulosViewModel, int> rolesMod;

        [BindProperty]
        public SeguridadRoles Roles { get; set; }

        [BindProperty]
        public SeguridadRolesModulosViewModel[] ModulosMios { get; set; }

        [BindProperty]
        public SeguridadRolesModulosViewModel[] Modulos { get; set; }

        [BindProperty]
        public SeguridadModulos[] ModulosMiosS { get; set; }

        [BindProperty]
        public SeguridadModulos[] ModulosS { get; set; }

        [BindProperty(SupportsGet = true)]
        public ParametrosFiltro filtro { get; set; }

        public RolesModulosModel(ICrudApi<SeguridadRoles, int> roles, ICrudApi<SeguridadModulos, int> modulos, ICrudApi<SeguridadRolesModulosViewModel, int> rolesMod)
        {
            this.roles = roles;
            this.modulos = modulos;
            this.rolesMod = rolesMod;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                Roles = await roles.ObtenerPorId(id);
        



                return Page();
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return Page();
            }
        }

        public async Task<IActionResult> OnGetModulosAsync(string id)
        {
            try
            {
                filtro.Cod = Convert.ToInt32(id);
                var ModulosGenerales = await modulos.ObtenerLista("");
                ModulosMios = await rolesMod.ObtenerLista(filtro);
               // Modulos = await rolesMod.ObtenerLista("");
                ModulosMiosS = new SeguridadModulos[ModulosMios.Length];
                //ModulosS = new SeguridadModulos[Modulos.Length];

                for(int j = 0; j < ModulosMiosS.Length; j++)
                {
                    ModulosMiosS[j] = new SeguridadModulos();
                }

                //for (int j = 0; j < ModulosS.Length; j++)
                //{
                //    ModulosS[j] = new SeguridadModulos();
                //}

                var i = 0;
                foreach (var item in ModulosMios)
                {
                    var Modulo = ModulosGenerales.Where(a => a.CodModulo == item.CodModulo).FirstOrDefault();
                    ModulosMiosS[i].CodModulo = Modulo.CodModulo;
                    ModulosMiosS[i].Descripcion = Modulo.Descripcion;

                        i++;

                }

                // i = 0;
                //foreach (var item in Modulos)
                //{
                //    var Modulo = ModulosGenerales.Where(a => a.CodModulo == item.CodModulo).FirstOrDefault();
                //    var existeEnElMio = ModulosMios.Where(a => a.CodModulo == Modulo.CodModulo).FirstOrDefault();
                //    if(existeEnElMio == null)
                //    {

                //        ModulosS[i].CodModulo = Modulo.CodModulo;
                //        ModulosS[i].Descripcion = Modulo.Descripcion;

                //        i++;
                //    }

                //}

                foreach(var item in ModulosMiosS)
                {
                    ModulosGenerales = ModulosGenerales.Where(a => a.CodModulo != item.CodModulo).ToArray();
                }



                var resp = new
                {
                    ModulosMiosS,
                    ModulosGenerales
                };



                return new JsonResult(resp);
            }
            catch (ApiException ex)
            {
                Errores error = JsonConvert.DeserializeObject<Errores>(ex.Content.ToString());
                ModelState.AddModelError(string.Empty, error.Message);

                return new JsonResult(error);
            }
        }
   
       
        public async Task<IActionResult> OnGetPost([FromBody] string recibidos)
        {
            try
            {
                recibidos = recibidos.Replace("_", " ");
                RecibidoRoles recibido = JsonConvert.DeserializeObject<RecibidoRoles>(recibidos);
                recibido.modulos = recibido.modulos.Replace("ProdCadenaM", "rolesMod");
                VectorRoles rolesModulos1 = JsonConvert.DeserializeObject<VectorRoles>(recibido.modulos);
                SeguridadRolesModulosViewModel[] rolesModulos = new SeguridadRolesModulosViewModel[rolesModulos1.rolesMod.Length];


                short cantidad = 0;
                if(rolesModulos.Length > 0)
                {

                foreach (var item in rolesModulos1.rolesMod)
                {

                    rolesModulos[cantidad] = new SeguridadRolesModulosViewModel();
                    rolesModulos[cantidad].CodRol = Convert.ToInt32(recibido.CodRol);
                        rolesModulos[cantidad].CodModulo = item.CodModulo;

                    cantidad++;
                }
                }
                else
                {
                    rolesModulos = new SeguridadRolesModulosViewModel[1];
                    rolesModulos[0].CodRol = Convert.ToInt32(recibido.CodRol);
                    rolesModulos[0].CodModulo = 0;
                }

                await rolesMod.AgregarBulk(rolesModulos);
                return RedirectToPage("./Index");
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
        //Esto es una prueba

        public async Task<IActionResult> OnPostGenerar( string recibidos)
        {
            try
            {
                recibidos = recibidos.Replace("_", " ");
                RecibidoRoles recibido = JsonConvert.DeserializeObject<RecibidoRoles>(recibidos);
                recibido.modulos = recibido.modulos.Replace("ProdCadenaM", "rolesMod");
                VectorRoles rolesModulos1 = JsonConvert.DeserializeObject<VectorRoles>(recibido.modulos);
                SeguridadRolesModulosViewModel[] rolesModulos = new SeguridadRolesModulosViewModel[rolesModulos1.rolesMod.Length];


                short cantidad = 0;
                if (rolesModulos.Length > 0)
                {

                    foreach (var item in rolesModulos1.rolesMod)
                    {

                        rolesModulos[cantidad] = new SeguridadRolesModulosViewModel();
                        rolesModulos[cantidad].CodRol = Convert.ToInt32(recibido.CodRol);
                        rolesModulos[cantidad].CodModulo = item.CodModulo;

                        cantidad++;
                    }
                }
                else
                {
                    rolesModulos = new SeguridadRolesModulosViewModel[1];
                    rolesModulos[0] = new SeguridadRolesModulosViewModel();
                    rolesModulos[0].CodRol = Convert.ToInt32(recibido.CodRol);
                    rolesModulos[0].CodModulo = 0;
                }

                await rolesMod.AgregarBulk(rolesModulos);
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
