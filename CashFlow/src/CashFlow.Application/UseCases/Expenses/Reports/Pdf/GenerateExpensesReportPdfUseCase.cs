
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExepensesReadOnlyRepository _exepensesReadOnlyRepository;
    public GenerateExpensesReportPdfUseCase(IExepensesReadOnlyRepository exepensesReadOnlyRepository)
    {
        _exepensesReadOnlyRepository = exepensesReadOnlyRepository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _exepensesReadOnlyRepository.FilterByMonth(month);
        if(expenses.Count == 0)
            return Array.Empty<byte>();
        return [];
    }
}
