using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ExpensesReadOnlyRepositoryBuilder
{
    private readonly Mock<IExepensesReadOnlyRepository> _exepensesReadOnlyRepository;
    public ExpensesReadOnlyRepositoryBuilder() 
    { 
        _exepensesReadOnlyRepository = new Mock<IExepensesReadOnlyRepository>();
    }
    public ExpensesReadOnlyRepositoryBuilder GetAll(User user, IList<Expense> expenses)
    {
        _exepensesReadOnlyRepository.Setup(repository => repository.GetAll(user)).ReturnsAsync(expenses);
        return this;
    }

    public IExepensesReadOnlyRepository Build() => _exepensesReadOnlyRepository.Object;
}
