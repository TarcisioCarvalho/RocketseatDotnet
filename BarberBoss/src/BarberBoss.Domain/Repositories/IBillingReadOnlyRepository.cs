using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingReadOnlyRepository
{
    Task<IEnumerable<Billing>> GetAll();
    Task<Billing?> GetById(Guid id);
}
