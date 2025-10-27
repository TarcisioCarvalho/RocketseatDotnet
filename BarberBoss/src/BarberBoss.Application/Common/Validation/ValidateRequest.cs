using BarberBoss.Application.UseCases.Billings;
using BarberBoss.Communication.Requests;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.Common.Validation;
public static class ValidateRequest
{
    public static void Validate(RequestBillingJson request)
    {
        var billingValidator = new BillingValidator();
        var validationResult = billingValidator.Validate(request);
        if ((!validationResult.IsValid))
        {
            throw new ErrorOnValidationException(validationResult.Errors);
        }
    }

    public static void ValidatePagination(RequestBillingsJson request)
    {
        var billingValidator = new BillingValidator();
        var validationResult = billingValidator.ValidadePagination(request);
        if ((!validationResult.IsValid))
        {
            throw new ErrorOnValidationException(validationResult.Errors);
        }
    }
    public static void ValidateFilters(RequestBillingsJson request)
    {
        var billingValidator = new BillingValidator();
        var validationResult = billingValidator.ValidateFilters(request);
        if ((!validationResult.IsValid))
        {
            throw new ErrorOnValidationException(validationResult.Errors);
        }
    }
    public static void ValidateFilters(RequestReportJson request)
    {
        var billingValidator = new BillingValidator();
        var validationResult = billingValidator.ValidateFilters(request);
        if ((!validationResult.IsValid))
        {
            throw new ErrorOnValidationException(validationResult.Errors);
        }
    }
}
