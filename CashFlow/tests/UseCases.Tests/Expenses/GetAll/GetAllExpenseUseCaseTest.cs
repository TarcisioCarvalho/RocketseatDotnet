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
    public async void Success()
    {
        var user = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(user, 5);
        var getAllExpenseUseCase = CreateGetAllExpenseUseCase(user, expenses);
        var result = await getAllExpenseUseCase.Execute();
        Assert.NotNull(result);
        // Tests for confirm unique id on expenses.
        Assert.Equal(result.Expenses.Count, result.Expenses.Select(e => e.Id).Distinct().Count());

    }

    private IGetAllExpenseUseCase CreateGetAllExpenseUseCase(User user, IList<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllExpenseUseCase(repository, mapper, loggedUser);
    }
}
