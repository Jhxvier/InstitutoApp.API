using Academico.Common.Interfaces;
using Academico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Repository
{
    public class EstudianteRepository : IGenericRepository<Estudiante>
    {
        public Task<Estudiante> ActualizarAsync(Estudiante entity)
        {
            throw new NotImplementedException();
        }

        public Task<Estudiante> CrearAsync(Estudiante entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Estudiante> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Estudiante>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
