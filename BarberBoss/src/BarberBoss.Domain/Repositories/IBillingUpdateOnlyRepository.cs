using BarberBoss.Domain.DTOs;
namespace BarberBoss.Domain.Repositories;
public interface IBillingUpdateOnlyRepository
{
    Task Update(Guid id, BillingUpdated billingUpdated);
}
