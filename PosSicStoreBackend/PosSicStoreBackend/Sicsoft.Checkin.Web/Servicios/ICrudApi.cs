using Refit;
using Sicsoft.Checkin.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Servicios
{


    public interface ICrudApi<TEntity, TKey> where TEntity : class
    {
        [Post("")]
        Task<TEntity> Agregar<TCreate>([Body] TCreate payload) where TCreate : class;



        [Post("")]
        Task<TEntity> Agregar([Body] TEntity payload);

        [Post("")]
        Task<TEntity[]> AgregarBulk([Body] TEntity[] payload);

        [Get("")]
        Task<TEntity[]> ObtenerLista<TQuery>(TQuery q);

        [Get("/CierreTarjetas")]
        Task<TEntity[]> ObtenerCierreTarjetas<TQuery>(TQuery q);

        [Get("/CierreCaja")]
        Task<TEntity[]> ObtenerCierre<TQuery>(TQuery q);

        [Get("/HistoricoVentas")]
        Task<TEntity[]> ObtenerHVentas<TQuery>(TQuery q);

        [Get("/MovimientoInventarios")]
        Task<TEntity[]> ObtenerMovimiento<TQuery>(TQuery q);

        [Get("/Compras")]
        Task<TEntity[]> ObtenerCompras<TQuery>(TQuery q);
        [Get("/EntradasySalidas")]
        Task<TEntity[]> ObtenerEntradasySalidas<TQuery>(TQuery q);
        [Get("/Kardex")]
        Task<TEntity[]> ObtenerKardez<TQuery>(TQuery q);
        [Get("/CatalogoProductos")]
        Task<TEntity[]> ObtenerListaCatalogoProductos<TQuery>(TQuery q);

        [Get("/VentasXHora")]
        Task<TEntity[]> ObtenerListaVentasXHora<TQuery>(TQuery q);

        [Get("/VentasResumen")]
        Task<TEntity[]> ObtenerListaVentasResumen<TQuery>(TQuery q);

        [Get("/VentasXProducto")]
        Task<TEntity[]> ObtenerListaVentasXProducto<TQuery>(TQuery q);

        [Get("/VentasXProductoFactura")]
        Task<TEntity[]> ObtenerListaVentasXProductoFactura<TQuery>(TQuery q);

        [Get("/Consultar")]
        Task<TEntity[]> ObtenerPorIdSeguridad(string id);


        [Get("/Consultar")]
        Task<TEntity> ObtenerPorId(string id);


        [Get("/Consultar")]
        Task<TEntity> ObtenerPorIdEncabezado(string id, string codMov);

        [Get("/Conectar")]
        Task<TEntity> Conectar(string codigo);
        [Get("")]
        Task<TEntity> IniciarSesion(string codigo, string clavePaso, string codCaja);



        [Get("/{key}")]
        Task<T> ObtenerPorId<T>(TKey key);

        [Put("/Actualizar")]
        Task Editar( [Body]TEntity payload);

       
        [Put("/{key}")]
        Task Editar<T>(TKey key, [Body]T payload);
        

        [Delete("/Eliminar")]
        Task Eliminar(string id);

        [Delete("/DesActivar")]
        Task DesActivar(string id);

        [Delete("/Eliminar")]
        Task EliminarInventario(string codMov, int NumDocto);

    }
}
