using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExepensesReadOnlyRepository
{
    Task<IList<Expense>> GetAll(Domain.Entities.User user);
    Task<Expense?> GetById(long id, Domain.Entities.User user);
    Task<IList<Expense>> FilterByMonth(DateOnly month);
}
