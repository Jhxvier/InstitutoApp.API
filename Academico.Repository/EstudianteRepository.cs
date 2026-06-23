using Academico.Common.Interfaces;
using Academico.Data;
using Academico.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Repository
{
    public class EstudianteRepository : IGenericRepository<Estudiante>
    {
        private readonly ApplicationDbContext _dbContext;

        public EstudianteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Estudiante> ActualizarAsync(Estudiante entity)
        {
            _dbContext.Estudiante.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Estudiante> CrearAsync(Estudiante entity)
        {
            _dbContext.Estudiante.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var estudiante = await ObtenerPorIdAsync(id);
            if (estudiante == null)
            {
                return false;
            }

            estudiante.Estado = false;
            _dbContext.Estudiante.Update(estudiante);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Estudiante> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Estudiante
                .AsNoTracking()
                .Where(estudiante => estudiante.IdEstudiante == id && estudiante.Estado)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Estudiante>> ObtenerTodosAsync()
        {
            return await _dbContext.Estudiante
                .AsNoTracking()
                .Where(estudiante => estudiante.Estado)
                .ToListAsync();
        }
    }

}
