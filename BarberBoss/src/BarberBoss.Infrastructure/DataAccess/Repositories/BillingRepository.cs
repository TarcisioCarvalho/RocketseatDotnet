using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Entitie;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess.Queries;
using Dapper;
using System.Data;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
public class BillingRepository : IBillingWriteOnlyRepository, IBillingReadOnlyRepository, IBillingUpdateOnlyRepository
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

    public async Task Delete(Guid id)
    {
        var p = new DynamicParameters();
        p.Add("Id", id);
        await _connection.ExecuteAsync(DeleteBillingQuery.Sql, p);
    }

    public async Task<(IEnumerable<BillingShort>, int, decimal)> GetAll(int page, int pageSize, DateTime? startDate, DateTime? endDate)
    {
        var offSet = (page - 1) * pageSize;
        var billings = await _connection.QueryAsync<BillingShort>(GetAllBillings.Query, new { OffSet = offSet, PageSize = pageSize, StartDate = startDate, EndDate = endDate });
        var totalBillings = await _connection.ExecuteScalarAsync<int>(GetAllBillings.CountQuery);
        var totalAmount = await _connection.ExecuteScalarAsync<decimal>(GetAllBillings.SumTotalAmountQuery, new { StartDate = startDate, EndDate = endDate });
        return (billings, totalBillings, totalAmount);
    }

    public async Task<IEnumerable<BillingReport>> GetBillingReport(DateTime? startDate, DateTime? endDate)
    {
        var billingsToReport = await _connection.QueryAsync<BillingReport>(GetBillingsToReportQuery.Query, new { StartDate = startDate, EndDate = endDate });
        return billingsToReport;
    }

    public async Task<Billing?> GetById(Guid id)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<Billing>(GetBillingByIdQuery.Query, new { Id = id });
        return result;
    }

    public async Task Update(Guid id, BillingUpdated billingUpdated)
    {
        var p = new DynamicParameters(billingUpdated);
        p.Add("Id", id);
        p.Add("PaymentMethod", billingUpdated.PaymentMethod.ToString());
        p.Add("Status", billingUpdated.Status.ToString());
        await _connection.ExecuteAsync(UpdateBillingQuery.Sql, p);
    }
}
