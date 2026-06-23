using Academico.Common.Interfaces;
using Academico.Data;
using Academico.DTOs.Matricula;
using Academico.Entities;
using Academico.Repository;
using Academico.Services;
using Academico.Services.Mapping;
using inaApp.DTOs.Curso;
using inaApp.DTOs.Estudiante;
using inaApp.DTOs.Matricula;
using Microsoft.EntityFrameworkCore;

namespace Academico.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicationServices
            (this IServiceCollection services,
            IConfiguration configuration)
        {

            //base de datos dbContext

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //profile auto mapper
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            //inyecciones de dependencia de servicios
            services.AddScoped<IGenericService<EstudianteResponseDTO, EstudianteCreateDTO, EstudianteUpdateDTO>, EstudianteService>();
            services.AddScoped<IGenericService<CursoResponseDTO, CursoCreateDTO, CursoUpdateDTO>, CursoService>();
            services.AddScoped<IGenericService<MatriculaResponseDTO, MatriculaCreateDTO, MatriculaUpdateDTO>, MatriculaService>();

            //inyeccion de dependencia repositorio
            services.AddScoped<IGenericRepository<Estudiante>, EstudianteRepository>();
            services.AddScoped<IGenericRepository<Curso>, CursoRepository>();
            services.AddScoped<IGenericRepository<Matricula>, MatriculaRepository>();

            return services;
        }
    }
}
