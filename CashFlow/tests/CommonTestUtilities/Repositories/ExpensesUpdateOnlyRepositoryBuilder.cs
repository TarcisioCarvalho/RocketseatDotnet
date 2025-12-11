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

    public IExpensesUpdateOnlyRepository Build() => _exepensesUpdateOnlyRepository.Object;
}
