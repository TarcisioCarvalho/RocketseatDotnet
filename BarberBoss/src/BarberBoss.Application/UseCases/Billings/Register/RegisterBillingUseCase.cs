using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Exception.ExceptionBase;


namespace BarberBoss.Application.UseCases.Billings.Register;
public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    public Task<ResponseRegisterBillingJson> Execute(RequestRegisterBillingJson request)
    {
        Validate(request);
        var billing = request.ToBilling();
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
