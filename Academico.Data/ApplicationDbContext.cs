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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Estudiante>()
                .HasIndex(estudiante => estudiante.Cedula)
                .IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasIndex(estudiante => estudiante.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<Curso>()
                .HasIndex(curso => curso.NombreCurso)
                .IsUnique();

            modelBuilder.Entity<Matricula>()
                .HasIndex(matricula => new { matricula.EstudianteId, matricula.CursoId })
                .IsUnique();

            modelBuilder.Entity<Matricula>()
                .HasOne(matricula => matricula.Estudiante)
                .WithMany(estudiante => estudiante.Matriculas)
                .HasForeignKey(matricula => matricula.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasOne(matricula => matricula.Curso)
                .WithMany(curso => curso.Matriculas)
                .HasForeignKey(matricula => matricula.CursoId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Matricula> Matricula { get; set; }
    }
}
