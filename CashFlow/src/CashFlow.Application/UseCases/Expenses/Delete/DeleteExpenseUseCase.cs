
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository expensesWriteOnlyRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _expensesWriteOnlyRepository = expensesWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }
    public async Task Execute(long id)
    {
        var user = await _loggedUser.Get();
        var result = await _expensesWriteOnlyRepository.Delete(id, user);
        if (result == false) throw new NotFoundException(ResourceErrorsMessages.EXPENSE_NOT_FOUND);
        await _unitOfWork.Commit();
    }

}
