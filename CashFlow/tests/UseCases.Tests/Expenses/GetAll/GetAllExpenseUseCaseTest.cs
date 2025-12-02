using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.GetAll;
public class GetAllExpenseUseCaseTest
{
    [Fact]
    public void Success()
    {
        var user = UserBuilder.Build();
    }

    private IGetAllExpenseUseCase CreateGetAllExpenseUseCase(User user, IList<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllExpenseUseCase(repository, mapper, loggedUser);
    }
}
