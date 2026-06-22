using Academico.Common.Interfaces;
using Academico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Repository
{
    public class MatriculaRepository : IGenericRepository<Matricula>
    {
        public Task<Matricula> ActualizarAsync(Matricula entity)
        {
            throw new NotImplementedException();
        }

        public Task<Matricula> CrearAsync(Matricula entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Matricula> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Matricula>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
