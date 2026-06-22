using Academico.Common.Interfaces;
using Academico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Repository
{
    public class CursoRepository : IGenericRepository<Curso>
    {
        public Task<Curso> ActualizarAsync(Curso entity)
        {
            throw new NotImplementedException();
        }

        public Task<Curso> CrearAsync(Curso entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Curso> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Curso>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
