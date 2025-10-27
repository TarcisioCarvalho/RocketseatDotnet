using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingReadOnlyRepository
{
    Task<(IEnumerable<BillingShort>, int, decimal)> GetAll(int page, int pageSize, DateTime? startDate, DateTime? endDate);
    Task<Billing?> GetById(Guid id);
    Task<IEnumerable<BillingReport>> GetBillingReport(DateTime? startDate, DateTime? endDate);
}
