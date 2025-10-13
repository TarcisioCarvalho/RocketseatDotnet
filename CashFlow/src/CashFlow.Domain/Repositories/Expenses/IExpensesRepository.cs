using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesRepository
{
    Task Add(Expense expense);
    Task<IList<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
