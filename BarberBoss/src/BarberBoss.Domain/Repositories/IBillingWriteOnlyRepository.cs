using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingWriteOnlyRepository
{
    Task Add(Billing billing);
    Task Delete(Guid id);
}
