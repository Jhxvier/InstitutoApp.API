using Academico.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Interfaces
{
    public interface IGenericService <TResponse, TCreate, TUpdate>
    {
        Task<Response<List<TResponse>>> ObtenerTodosAsync();
        Task<Response<TResponse>> ObtenerPorIdAsync(int id);
        Task<Response<TResponse>> CrearAsync(TCreate entity);
        Task<Response<TResponse>> ActualizarAsync(TUpdate entity);
        Task<Response<bool>> EliminarAsync(int id);
    }
}
