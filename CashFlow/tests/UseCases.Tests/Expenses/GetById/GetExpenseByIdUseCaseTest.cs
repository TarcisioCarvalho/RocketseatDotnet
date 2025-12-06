using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.GetById;
public class GetExpenseByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user, 1);
        var useCase = CreateGetExpenseByIdUseCase(user, expense);
        var result = await useCase.Execute(expense.Id);
        Assert.NotNull(result);
        Assert.Equal(expense.Id, result.Id);
        Assert.Equal(expense.Title, result.Title);
        Assert.Equal(expense.Description, result.Description);
        Assert.Equal(expense.Value, result.Value);
        Assert.Equal(expense.PaymentType, (PaymentType)result.PaymentType);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var user = UserBuilder.Build();
        var useCase = CreateGetExpenseByIdUseCase(user, null);
        var result = useCase.Execute(1000);
        //Assert.Null(result);
        var ex =  await Assert.ThrowsAsync<NotFoundException>(() => result);
        var erros = ex.GetErrors();
        Assert.NotNull(erros);
        Assert.Single(erros);
        Assert.True(erros.Contains(ResourceErrorsMessages.EXPENSE_NOT_FOUND));
    }

    private IGetExpenseByIdUseCase CreateGetExpenseByIdUseCase(User user, Expense? expense)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
    }
}

