using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;
using MRent.Infrastructure.Repositories;

namespace MRent.Infrastructure.Injectors
{
    public static class DataInjector
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PostgresContext>(options => options.UseNpgsql(connectionString));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Repositories
            services.AddScoped<IMotorcycleRepository, MotorcicleRepository>();
            services.AddScoped<IMotorcycleLogRepository, MotorcicleLogRepository>();
            services.AddScoped<ICourierRepository, CourierRepository>();
            services.AddScoped<IRentRepository, RentRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();

            return services;
        }
    }
}
