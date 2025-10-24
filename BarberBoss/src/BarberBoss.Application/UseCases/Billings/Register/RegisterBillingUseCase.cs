using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception.ExceptionBase;


namespace BarberBoss.Application.UseCases.Billings.Register;
public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _billingWriteOnlyRepository;
    public RegisterBillingUseCase(IBillingWriteOnlyRepository billingWriteOnlyRepository)
    {
        _billingWriteOnlyRepository = billingWriteOnlyRepository;
    }
    public async Task<ResponseRegisterBillingJson> Execute(RequestRegisterBillingJson request)
    {
        Validate(request);
        var billing = request.ToBilling();
        await  _billingWriteOnlyRepository.Add(billing);
        return billing.ToResponseRegisterBillingJson();
    }

    private void Validate(RequestRegisterBillingJson request)
    {
        var billingValidator = new BillingValidator();
        var validationResult = billingValidator.Validate(request);
        if ((!validationResult.IsValid))
        {
            throw new ErrorOnValidationException(validationResult.Errors);
        }
    }
}
