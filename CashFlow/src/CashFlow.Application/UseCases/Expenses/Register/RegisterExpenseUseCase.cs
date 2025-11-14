using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;


namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);
        // Implementation of the use case to register an expense

        var entity = _mapper.Map<Expense>(request);
        var user = await _loggedUser.Get();
        entity.UserId = user.Id;
        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(entity);
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
