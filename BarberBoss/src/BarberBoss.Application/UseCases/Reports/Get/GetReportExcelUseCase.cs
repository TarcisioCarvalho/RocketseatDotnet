using BarberBoss.Application.Common.Validation;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using ClosedXML.Excel;

namespace BarberBoss.Application.UseCases.Reports.Get;
public class GetReportExcelUseCase : IGetReportExcelUseCase
{
    private readonly IBillingReadOnlyRepository _billingReadOnlyRepository;
    public GetReportExcelUseCase(IBillingReadOnlyRepository billingReadOnlyRepository)
    {
        _billingReadOnlyRepository = billingReadOnlyRepository;
    }
    public async Task<byte[]> Execute(RequestReportJson request)
    {
        ValidateRequest.ValidateFilters(request);
        
        var billingsToReport = await _billingReadOnlyRepository.GetBillingReport(request.StartDate, request.EndDate);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Billings");

        // Cabeçalho
        worksheet.Cell("A1").Value = "Título";
        worksheet.Cell("B1").Value = "Data";
        worksheet.Cell("C1").Value = "Tipo de pagamento";
        worksheet.Cell("D1").Value = "Valor";
        worksheet.Cell("E1").Value = "Descrição";

        // Formatar cabeçalho (verde escuro + branco + negrito)
        var headerRange = worksheet.Range("A1:E1");
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1e5631");
        headerRange.Style.Font.FontColor = XLColor.White;
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        // Preencher dados
        int currentRow = 2;
        foreach (var billing in billingsToReport)
        {
            worksheet.Cell($"A{currentRow}").Value = billing.ServiceName;
            worksheet.Cell($"B{currentRow}").Value = billing.Date.ToString("dd/MM/yyyy");
            worksheet.Cell($"C{currentRow}").Value = billing.PaymentMethod.ToString();
            worksheet.Cell($"D{currentRow}").Value = $"R$ {billing.Amount:N2}";
            worksheet.Cell($"E{currentRow}").Value = billing.Notes;

            currentRow++;
        }

        // Ajustar largura das colunas automaticamente
        worksheet.Columns().AdjustToContents();

        // Converter para byte array
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
