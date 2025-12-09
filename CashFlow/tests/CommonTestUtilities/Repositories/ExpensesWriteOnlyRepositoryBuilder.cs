using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories;
public static class ExpensesWriteOnlyRepositoryBuilder
{
    public static IExpensesWriteOnlyRepository Build()
    {
        return new Moq.Mock<IExpensesWriteOnlyRepository>().Object;
    }

    public static IExpensesWriteOnlyRepository BuildDeleteUseCase(long id, User user)
    {
        var mock = new Moq.Mock<IExpensesWriteOnlyRepository>();
        mock.Setup(repository => repository.Delete(id,user)).ReturnsAsync(true);
        return mock.Object;
    }
}
