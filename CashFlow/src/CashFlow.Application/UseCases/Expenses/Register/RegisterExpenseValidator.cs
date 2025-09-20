using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ResourceErrorsMessages.TITLE_REQUIRED)
            .MaximumLength(100).WithMessage(ResourceErrorsMessages.TITLE_CHARACTERS_LIMIT);
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(ResourceErrorsMessages.DESCRIPTION_CHARACTERS_LIMIT);
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage(ResourceErrorsMessages.DATE_REQUIRED)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(ResourceErrorsMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage(ResourceErrorsMessages.VALUE_MUST_BE_GRATER_THAN_ZERO);
        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage(ResourceErrorsMessages.PAYMENT_TYPE_INVALID);
    }

  
}
