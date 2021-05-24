using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Servicios;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sicsoft.Checkin.Web
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ICrudApi<CompaniaLoginViewModel, string> service;
        private readonly ICrudApi<CajaViewModel, string> serviceCaja;
        private readonly ICrudApi<CierreCajaViewModel, string> CierreCaja;
        private readonly ICrudApi<SeguridadUsuarioViewModel, string> Usuarios;
        private readonly ICrudApi<SeguridadRolesModulosViewModel, int> modulos;
        //private readonly ICheckInClient checkInService;

        [BindProperty]
        public string Token { get; set; }
        [BindProperty]
        public string NombreCajero { get; set; }
        [BindProperty]
        public string Seguridad { get; set; }
        [BindProperty]
        public SeguridadUsuarioViewModel Cierre { get; set; }
        public IConfiguration Configuration { get; }

        public LoginModel(ICrudApi<CompaniaLoginViewModel, string> service, ICrudApi<SeguridadRolesModulosViewModel, int> modulos,ICrudApi<SeguridadUsuarioViewModel, string> Cajero, ICrudApi<CajaViewModel, string> serviceCaja, ICrudApi<CierreCajaViewModel, string> CierreCaja, IConfiguration configuration)
        {
            this.service = service;
            this.Usuarios = Cajero;
            this.serviceCaja = serviceCaja;
            this.CierreCaja = CierreCaja;
            this.Configuration = configuration;
            this.modulos = modulos;
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    await HttpContext.SignOutAsync();
            //    return Page();
            //}
            ActionResult response = Page();
            try
            {
                //Aca va la parte de codigo para saber que cliente trata de ingresar
 

                

                //var sp1 = await checkInServiceTLogin.ObtenerPantalla(pl);

             


                   // var sp = await checkInServiceTLogin.ObtenerLista(pl);

                

                try
                {
                    //var SeguridadModulos = await modulos.ObtenerPorIdSeguridad(Cierre.NomUsuario);

                    //var seguridad = "";

                    //foreach(var item in SeguridadModulos)
                    //{
                    //    seguridad += item.CodModulo + "|";
                    //}

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, NombreCajero));
                    identity.AddClaim(new Claim(ClaimTypes.Email, Cierre.NomUsuario));
                    identity.AddClaim(new Claim(ClaimTypes.UserData, Token));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Seguridad));


                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    //var cajero = await Cajero.ObtenerPorId(Cierre.CodCajero);

                    //Constantes.urlwebapi = Token;
                 //var s =   await HacerCierre(Cierre);
                  
                    //if(!s)
                    //{
                    //    throw new Exception("Error al enviar el cierre de caja");
                    //}
                    return RedirectToPage("/Index");




                }
                catch (ValidationApiException)
                {
                    // handle validation here by using validationException.Content,
                    // which is type of ProblemDetails according to RFC 7807

                    // If the response contains additional properties on the problem details,
                    // they will be added to the validationException.Content.Extensions collection.
                }
                catch (ApiException exception)
                {
                    if (exception.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Email", "Usuario o contraseña invalida");
                     
                        return Page();
                    }
                   
                }
                
                }
                catch(ApiException ex)
                {
                  // ModelState.AddModelError("Email", "Usuario o contraseña invalida");
                  
                    return Page();
                }


            return response;


        }
        public async Task<bool> HacerCierre(CierreCajaViewModel Cierre)
        {
            

                try
                {


                    Cierre.FecCaja = DateTime.Now;
                    Cierre.CodSupervisor = "";
                    Cierre.Efectivo = 0;
                    Cierre.Cheques = 0;
                    Cierre.Tarjetas = 0;
                    Cierre.OtrosPagos = 0;
                    Cierre.MtoVendido = 0;
                    Cierre.Diferencia = 0;
                    Cierre.Abierta = true;
                    Cierre.HoraCierre = null;
                    Cierre.MontoApertura = 0;
                    Cierre.CortesEfectivo = 0;
                    Cierre.CortesDescripcion = "";
                    Cierre.NumCierre = 0;
                    Cierre.Activa = true;
                    Cierre.Dolares = 0;
                    Cierre.TipoCambio = 0;
                    Cierre.IP = "";


                    await CierreCaja.Editar(Cierre);
                Constantes.urlwebapi = null;
            }
                
                catch (ApiException exception)
                {
                return false;
                    if (exception.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                       

                         
                    }

                }

            
          

                return true;

            


        }

       

        public async Task<IActionResult> OnGetConectar(string codigo)
        {
            try
            {
              

                var sp = await service.Conectar(codigo);
 
                return new JsonResult(sp);

            }
            catch (ApiException ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        public async Task<IActionResult> OnGetBuscarCaja(string codigo)
        {
            try
            {


                var sp = await serviceCaja.Conectar(codigo);

                return new JsonResult(sp);

            }
            catch (ApiException ex)
            {
                return new JsonResult(ex.Message);
            }

        }
    }
}