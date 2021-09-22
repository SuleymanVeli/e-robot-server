using e_robot.Application.Contracts.Persistence;
using e_robot.Infrastructure.Persistence;
using e_robot.Infrastructure.Repositories;
using e_robot.Infrastructure.Repositories.Dapper;
using e_robot.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace e_robot.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataBaseContext>(options => options.UseMySql(configuration.GetConnectionString("ConnectionString"), ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionString"))));
                        
            services.AddScoped<DbContext, DataBaseContext>();
            services.AddScoped<IAsyncRepository, GenericMapperRepository>();
            services.AddScoped<IDapper, DapperClass>();            
            
            return services;
        }
    }
}
