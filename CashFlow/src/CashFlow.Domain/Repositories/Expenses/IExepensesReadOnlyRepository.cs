using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExepensesReadOnlyRepository
{
    Task<IList<Expense>> GetAll();
    Task<Expense?> GetById(long id);
    Task<IList<Expense>> FilterByMonth(DateOnly month);
}
