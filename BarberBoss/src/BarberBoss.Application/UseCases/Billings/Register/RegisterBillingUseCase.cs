using BarberBoss.Application.Common.Validation;
using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;


namespace BarberBoss.Application.UseCases.Billings.Register;
public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _billingWriteOnlyRepository;
    public RegisterBillingUseCase(IBillingWriteOnlyRepository billingWriteOnlyRepository)
    {
        _billingWriteOnlyRepository = billingWriteOnlyRepository;
    }
    public async Task<ResponseRegisterBillingJson> Execute(RequestBillingJson request)
    {
        ValidateRequest.Validate(request);
        var billing = request.ToBilling();
        await  _billingWriteOnlyRepository.Add(billing);
        return billing.ToResponseRegisterBillingJson();
    }
}
