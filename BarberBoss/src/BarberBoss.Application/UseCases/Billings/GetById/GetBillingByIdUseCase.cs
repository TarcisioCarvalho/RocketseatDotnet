using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception.ExceptionBase;


namespace BarberBoss.Application.UseCases.Billings.GetById;
public class GetBillingByIdUseCase : IGetBillingByIdUseCase
{
    private readonly IBillingReadOnlyRepository _billingReadOnlyRepository;
    public GetBillingByIdUseCase(IBillingReadOnlyRepository billingReadOnlyRepository)
    {
        _billingReadOnlyRepository = billingReadOnlyRepository;
    }
    public async Task<ResponseBillingJson> Execute(Guid id)
    {
        var billing = await _billingReadOnlyRepository.GetById(id);
        if (billing is null)
            throw new NotFoundException("Billing not found.");
       var billingResponse = billing.ToResponseBillingJson();
       return billingResponse;
    }
}
