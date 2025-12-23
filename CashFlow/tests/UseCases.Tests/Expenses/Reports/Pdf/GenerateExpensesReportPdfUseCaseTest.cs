using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);
        var useCase = CreateUseCase(loggedUser, expenses);
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Now));
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser, []);
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Now));
        Assert.Empty(result);
    }
    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, IList<Expense> expenses)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
    }
}
