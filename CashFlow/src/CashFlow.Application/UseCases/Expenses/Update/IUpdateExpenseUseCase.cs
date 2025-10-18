using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCase
{
    Task Execute(RequestExpenseJson expense,long id);
}
