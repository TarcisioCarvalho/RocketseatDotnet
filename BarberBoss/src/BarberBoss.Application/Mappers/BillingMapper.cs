using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Entitie;

namespace BarberBoss.Application.Mappers;
public static class BillingMapper
{
    public static Billing ToBilling(this RequestBillingJson request)
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
    public static BillingUpdated ToBillingUpdate(this RequestBillingJson request)
    {
        return new BillingUpdated
        {
            Date = request.Date,
            BarberName = request.BarberName,
            ClientName = request.ClientName,
            ServiceName = request.ServiceName,
            Amount = request.Amount,
            PaymentMethod = (Domain.Enums.PaymentMethod)(int)request.PaymentMethod,
            Status = (Domain.Enums.Status)(int)request.Status,
            Notes = request.Notes,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    public static ResponseRegisterBillingJson ToResponseRegisterBillingJson(this Billing billing)
    {
        return new ResponseRegisterBillingJson
        {
            Id = billing.Id,
        };
    }

    public static ResponseShortBillingJson ToResponseShortBillingJson(this BillingShort billingShort)
    {
        return new ResponseShortBillingJson
        {
            Id = billingShort.Id,
            Date = billingShort.Date,
            Status = (Communication.Enums.Status)(int)billingShort.Status
        };
    }

    public static ResponseBillingsJson ToResponseBillingsJson(this IEnumerable<BillingShort> billingShorts, int page, int PageSize, int total, decimal totalAmount)
    {
        var response = new ResponseBillingsJson
        {
            Billings = billingShorts.Select(bs => bs.ToResponseShortBillingJson()).ToList(),
            Page = page,
            PageSize = PageSize,
            Total = total,
            TotalPages = (int)Math.Ceiling((double)total / PageSize),
            HasNextPage = page < (int)Math.Ceiling((double)total / PageSize),
            HasPreviousPage = page > 1,
            TotalAmount = totalAmount
        };
        return response;
    }

    public static ResponseBillingJson ToResponseBillingJson(this Billing billing)
    {
        return new ResponseBillingJson
        {
            Id = billing.Id,
            Date = billing.Date,
            BarberName = billing.BarberName,
            ClientName = billing.ClientName,
            ServiceName = billing.ServiceName,
            Amount = billing.Amount,
            PaymentMethod = (Communication.Enums.PaymentMethod)(int)billing.PaymentMethod,
            Status = (Communication.Enums.Status)(int)billing.Status,
            Notes = billing.Notes,
            CreatedAt = billing.CreatedAt,
            UpdatedAt = billing.UpdatedAt
        };
    }
}