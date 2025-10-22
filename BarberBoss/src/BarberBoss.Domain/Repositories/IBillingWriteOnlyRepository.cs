using BarberBoss.Domain.Entitie;

namespace BarberBoss.Domain.Repositories;
public interface IBillingWriteOnlyRepository
{
    Task<bool> Add(Billing billing);
}
