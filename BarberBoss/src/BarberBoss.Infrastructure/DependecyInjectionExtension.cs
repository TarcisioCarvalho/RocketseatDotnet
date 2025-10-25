using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace BarberBoss.Infrastructure;
public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IDbConnection>(sp =>
          new Npgsql.NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBillingWriteOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingReadOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingUpdateOnlyRepository, BillingRepository>();
    }
}

