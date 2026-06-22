using Academico.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //fluent API





        public DbSet<Estudiante> Estudiante {  get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Matricula> Matricula { get; set; }


    }
}
