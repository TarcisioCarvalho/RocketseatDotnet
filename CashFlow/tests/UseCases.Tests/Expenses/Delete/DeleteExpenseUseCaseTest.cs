using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.Delete;
public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user, 4);
        var useCase = CreateDeDeleteExpenseUseCase(expense.Id, user);
        await useCase.Execute(expense.Id);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user, 4);
        var useCase = CreateDeDeleteExpenseUseCase(2, user);
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            async () => await useCase.Execute(4)
            );
        Assert.Single(exception.GetErrors());
        Assert.Contains(ResourceErrorsMessages.EXPENSE_NOT_FOUND, exception.GetErrors());
    }

    private IDeleteExpenseUseCase CreateDeDeleteExpenseUseCase(long id, User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var writeRepository = ExpensesWriteOnlyRepositoryBuilder.BuildDeleteUseCase(id, user);

        return new DeleteExpenseUseCase(writeRepository, unitOfWork, loggedUser);
    }
}
