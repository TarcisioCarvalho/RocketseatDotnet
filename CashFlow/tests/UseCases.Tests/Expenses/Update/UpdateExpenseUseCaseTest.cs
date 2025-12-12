using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;

namespace UseCases.Tests.Expenses.Update;
public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var expense = ExpenseBuilder.Build(user, null);

        var useCase = CreateUpdateUseCase(user, expense);
        await useCase.Execute(request, expense.Id);

        Assert.Equal(expense.Title, request.Title);
        Assert.Equal(request.Description, expense.Description);
        Assert.Equal(request.Value, expense.Value);
        Assert.Equal((PaymentType)request.PaymentType, expense.PaymentType);
        Assert.Equal(request.Date, expense.Date);

    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var user = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var expense = ExpenseBuilder.Build(user, null);
        request.Title = string.Empty;

        var useCase = CreateUpdateUseCase(user, expense);
        
        Func<Task> act = async () => await useCase.Execute(request, expense.Id);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => act());

        Assert.Single(exception.GetErrors());
        Assert.Equal(ResourceErrorsMessages.TITLE_REQUIRED, exception.GetErrors().First());
    }
    private IUpdateExpenseUseCase CreateUpdateUseCase(User user, Expense? expense)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();
        var repository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(expense: expense, user: user).Build();

        return new UpdateExpenseUseCase(expenseRepository: repository, unitOfWork: unitOfWork, mapper: mapper, loggedUser: loggedUser);
    }
}
