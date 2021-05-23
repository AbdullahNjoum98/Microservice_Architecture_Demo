using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TeacherMicroservice.ServicesConfig
{
    public static class ServicesConfigurator
    {
        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("TeacherMicroserviceDB");


            services.AddDbContext<ProjectDBContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("TeacherMicroservice").UseNetTopologySuite()));


            return services;
        }
    }
}
