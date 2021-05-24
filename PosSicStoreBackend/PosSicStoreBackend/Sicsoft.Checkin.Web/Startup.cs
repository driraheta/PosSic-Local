using System;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Sicsoft.Checkin.Web.Models;
using Sicsoft.Checkin.Web.Models.Compras;
using Sicsoft.Checkin.Web.Servicios;

namespace Sicsoft.Checkin.Web
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromMinutes(300);
               
           });

            services.AddHttpContextAccessor();

            services.AddScoped<AuthenticatedHttpClientHandler>();

            services.AddApiServices(Configuration);
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            
            services.AddRazorPages()
                .AddMvcOptions(options =>
                {

                    options.Filters.Add(new AuthorizeFilter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode", "?code={0}");

            app.UseHttpsRedirection();

            var supportedCultures = new[]
            {
                new CultureInfo("es-CR"), //es-cr

            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-CR"),

                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }

    public static class CheckinServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration Configuration )
        {


            services.AddRefitClient<ICrudApi<CompaniaLoginViewModel, string>>()
                   .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Compañias"));
            // Add additional IHttpClientBuilder chained methods as required here:
            //.AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
            // .SetHandlerLifetime(TimeSpan.FromMinutes(2));

            services.AddRefitClient<ICrudApi<CajaViewModel, string>>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Caja"));
            //.AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
            // .SetHandlerLifetime(TimeSpan.FromMinutes(2));


            services.AddRefitClient<ICrudApi<UsuarioViewModel, string>>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Cajeros"))
             .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<CierreCajaViewModel, string>>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/CierreCaja"))
          .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<SeguridadUsuarioViewModel, string>>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Login"))
          .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<LineasViewModel, string>>()
         .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Lineas"))
       .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<ProveedoresViewModel, string>>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Proveedores"))
      .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<ProductosViewModel, string>>()
      .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Productos"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CabysViewModel, string>>()
   .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Cabys"))
 .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<CodigosTarifaImpuestosViewModel, string>>()
   .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/CodigosTarifaImpuestos"))
 .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<ParametrosViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Parametros"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<ClientesViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Clientes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<TiposPreciosViewModel, int>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/TiposPrecios"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CedulasViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Cedulas"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<TiposMovimientosViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/TiposMovimientos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();



            services.AddRefitClient<ICrudApi<CajasViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Cajas"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CajerosViewModel, string>>()
 .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Cajeros"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<VendedorViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Vendedores"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<BancosViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Bancos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<TarjetasViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Tarjetas"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<TiposPagoViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/TiposPagos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<SeguridadRolesModulosViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/SeguridadRolesModulos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<MovimientoInventariosViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/MovimientoInventario"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<EncMovInvViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/EncMovInv"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<DetMovInvViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/DetMovIn"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<ComprasR, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Compras"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<EncCompras, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/EncCompras"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<DetCompras, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/DetCompras"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddRefitClient<ICrudApi<SeguridadRoles, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/SeguridadRoles"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<SeguridadModulos, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/SeguridadModulos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<AbonosViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Abonos"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<EncApartadosViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/EncApto"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<EncVtasViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/EncVtas"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<HistoricoVentasViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/HistVentas"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

//            services.AddRefitClient<ICrudApi<CierreCajaViewModel, string>>()
//.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/CierreCaja"))
//.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<VentasResumenViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<VentasXProductoViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<VentasXHoraViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CatalogoProductosViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<ComprasReportes, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<MovimientoInventariosReportes, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<HistoricoVentasReportes, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CierreCajaReportesViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CierreTarjetasReportes, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<ReportesProductosViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<KardexViewModel, string>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<VentasProductosFacturas, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/Reportes"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<CierreDiarioViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/CierreDiario"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<ICrudApi<TipoCambioViewModel, int>>()
.ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Configuration["CheckInAPIEndpoint"]}/api/TipoCambio"))
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
            return services;
        }
    }
}
