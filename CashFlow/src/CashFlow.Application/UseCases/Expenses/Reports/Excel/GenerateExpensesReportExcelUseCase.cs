using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private readonly IExepensesReadOnlyRepository _exepensesReadOnlyRepository;
    public GenerateExpensesReportExcelUseCase(IExepensesReadOnlyRepository exepensesReadOnlyRepository)
    {
        _exepensesReadOnlyRepository = exepensesReadOnlyRepository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _exepensesReadOnlyRepository.FilterByMonth(month);
        if(expenses.Count == 0)
            return [];
        var workbook = new XLWorkbook();
        workbook.Author = "CashFlow Application";
        workbook.Style.Font.FontName = "Segoe UI";
        workbook.Style.Font.FontSize = 12;
        
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsertHeader(worksheet);
        foreach (var expense in expenses)
        {
            var lastRow = worksheet.LastRowUsed().RowNumber() + 1;
            worksheet.Cell($"A{lastRow}").Value = expense.Title;
            worksheet.Cell($"B{lastRow}").Value = expense.Date.ToString("yyyy-MM-dd");
            worksheet.Cell($"C{lastRow}").Value = ConvertPaymentType(expense.PaymentType);
            worksheet.Cell($"D{lastRow}").Value = expense.Value;
            worksheet.Cell($"D{lastRow}").Style.NumberFormat.Format = "$ #,##0.00";
            worksheet.Cell($"E{lastRow}").Value = expense.Description;
        }
        var file = new MemoryStream();
        workbook.SaveAs(file);  
        return file.ToArray();
    }

    private string ConvertPaymentType(PaymentType paymentType)
    {
               return paymentType switch
        {
            PaymentType.cash => "Cash",
            PaymentType.creditCard => "Credit Card",
            PaymentType.debitCard => "Debit Card",
            PaymentType.electronicTransfer => "Eletronic Transfer",
            _ => "Unknown"
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = "TITLE";
        worksheet.Cell("B1").Value = "DATE";
        worksheet.Cell("C1").Value = "PAYMENT_TYPE";
        worksheet.Cell("D1").Value = "AMOUNT";
        worksheet.Cell("E1").Value = "DESCRIPTION";

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C286");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
