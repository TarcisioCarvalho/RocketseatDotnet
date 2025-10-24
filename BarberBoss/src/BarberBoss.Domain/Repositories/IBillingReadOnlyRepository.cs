using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingReadOnlyRepository
{
    Task<IEnumerable<BillingShort>> GetAll();
    Task<Billing?> GetById(Guid id);
}
