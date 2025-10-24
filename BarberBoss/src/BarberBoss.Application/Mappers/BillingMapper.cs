using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Entitie;

namespace BarberBoss.Application.Mappers;
public static class BillingMapper
{
    public static Billing ToBilling(this RequestRegisterBillingJson request)
    {
        return new Billing
        {
            Id = Guid.NewGuid(),
            Date = request.Date,
            BarberName = request.BarberName,
            ClientName = request.ClientName,
            ServiceName = request.ServiceName,
            Amount = request.Amount,
            PaymentMethod = (Domain.Enums.PaymentMethod)(int)request.PaymentMethod,
            Status = (Domain.Enums.Status)(int)request.Status,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }
}