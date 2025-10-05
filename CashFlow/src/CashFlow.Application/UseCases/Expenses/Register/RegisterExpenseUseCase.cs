using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;


namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;

    public RegisterExpenseUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        // Implementation of the use case to register an expense
        
        var expense = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Value = request.Value,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };
        _repository.Add(expense);

        return new ResponseRegisterExpenseJson
        {
            Title = request.Title,
        };
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors =  validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
