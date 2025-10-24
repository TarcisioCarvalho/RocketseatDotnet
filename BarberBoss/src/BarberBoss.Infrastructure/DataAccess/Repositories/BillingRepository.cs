using BarberBoss.Domain.Entitie;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess.Queries;
using Dapper;
using System.Data;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
public class BillingRepository : IBillingWriteOnlyRepository
{
    private readonly IDbConnection _connection;

    public BillingRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task Add(Billing billing)
    {
        var p = new DynamicParameters(billing);

        p.Add("PaymentMethod", billing.PaymentMethod.ToString());
        p.Add("Status", billing.Status.ToString());

        await _connection.ExecuteAsync(InsertBillingQuery.Query, p);
    }
}
