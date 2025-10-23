using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace BarberBoss.Infrastructure;
public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnection>(sp =>
            new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
    }
}

