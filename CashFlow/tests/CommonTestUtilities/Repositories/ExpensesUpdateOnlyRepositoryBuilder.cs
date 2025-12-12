using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ExpensesUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IExpensesUpdateOnlyRepository> _exepensesUpdateOnlyRepository;

    public ExpensesUpdateOnlyRepositoryBuilder()
    {
        _exepensesUpdateOnlyRepository = new Mock<IExpensesUpdateOnlyRepository>();
    }

    public ExpensesUpdateOnlyRepositoryBuilder GetById(Expense? expense, User user)
    {
        if(expense is not null)
        _exepensesUpdateOnlyRepository.Setup(repo => repo.GetById(expense.Id, user)).ReturnsAsync(expense);
        return this;
    }

    public IExpensesUpdateOnlyRepository Build() => _exepensesUpdateOnlyRepository.Object;
}
