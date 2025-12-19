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

    public ExpensesReadOnlyRepositoryBuilder GetById(User user, Expense? expense)
    {
        if (expense is not null)
            _exepensesReadOnlyRepository.Setup(repository => repository.GetById(expense.Id, user)).ReturnsAsync(expense);

        return this;
    }

    public ExpensesReadOnlyRepositoryBuilder FilterByMonth(User user,List<Expense> expenses)
    {
        _exepensesReadOnlyRepository.Setup(repository => repository.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);
        return this;
    }

    public IExepensesReadOnlyRepository Build() => _exepensesReadOnlyRepository.Object;
}
