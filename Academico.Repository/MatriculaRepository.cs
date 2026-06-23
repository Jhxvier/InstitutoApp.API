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
    public class MatriculaRepository : IGenericRepository<Matricula>
    {
        private readonly ApplicationDbContext _dbContext;

        public MatriculaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Matricula> ActualizarAsync(Matricula entity)
        {
            _dbContext.Matricula.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Matricula> CrearAsync(Matricula entity)
        {
            _dbContext.Matricula.Add(entity);
            await _dbContext.SaveChangesAsync();
            return await ObtenerPorIdAsync(entity.IdMatricula);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var matricula = await ObtenerPorIdAsync(id);
            if (matricula == null)
            {
                return false;
            }

            matricula.Estado = false;
            _dbContext.Matricula.Update(matricula);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Matricula> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Matricula
                .AsNoTracking()
                .Include(matricula => matricula.Estudiante)
                .Include(matricula => matricula.Curso)
                .Where(matricula => matricula.IdMatricula == id && matricula.Estado)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Matricula>> ObtenerTodosAsync()
        {
            return await _dbContext.Matricula
                .AsNoTracking()
                .Include(matricula => matricula.Estudiante)
                .Include(matricula => matricula.Curso)
                .Where(matricula => matricula.Estado)
                .ToListAsync();
        }
    }

}
