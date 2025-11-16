using CashFlow.Domain.Repositories.Expenses;

namespace CommonTestUtilities.Repositories;
public static class ExpensesWriteOnlyRepositoryBuilder
{
    public static IExpensesWriteOnlyRepository Build()
    {
        return new Moq.Mock<IExpensesWriteOnlyRepository>().Object;
    }
}
