using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingReadOnlyRepository
{
    Task<(IEnumerable<BillingShort>, int)> GetAll(int page, int pageSize);
    Task<Billing?> GetById(Guid id);
}
