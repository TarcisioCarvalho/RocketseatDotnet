using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCaseTest
{
    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
    }
}
