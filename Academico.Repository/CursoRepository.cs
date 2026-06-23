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
    public class CursoRepository : IGenericRepository<Curso>
    {
        private readonly ApplicationDbContext _dbContext;

        public CursoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Curso> ActualizarAsync(Curso entity)
        {
            _dbContext.Curso.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Curso> CrearAsync(Curso entity)
        {
            _dbContext.Curso.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var curso = await ObtenerPorIdAsync(id);
            if (curso == null)
            {
                return false;
            }

            curso.Estado = false;
            _dbContext.Curso.Update(curso);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Curso> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Curso
                .AsNoTracking()
                .Where(curso => curso.IdCurso == id && curso.Estado)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Curso>> ObtenerTodosAsync()
        {
            return await _dbContext.Curso
                .AsNoTracking()
                .Where(curso => curso.Estado)
                .ToListAsync();
        }
    }

}
