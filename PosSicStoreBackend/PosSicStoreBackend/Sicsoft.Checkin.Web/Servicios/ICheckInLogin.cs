using Refit;
using Sicsoft.Checkin.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicsoft.Checkin.Web.Servicios
{
    public interface ICheckInLogin<TEntity, TKey> where TEntity : class
    {
        [Get("")]
        Task<TEntity> ObtenerLista<TQuery>(TQuery q);

 
    }
}
