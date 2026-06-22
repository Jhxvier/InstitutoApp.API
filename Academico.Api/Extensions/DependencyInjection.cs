using Academico.Data;
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
            //services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            return services;
        }
    }
}
