using BarberBoss.Application.UseCases.Reports.Get;
using BarberBoss.Communication.Requests;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    /// <summary>
    /// Exporta relatório de billings em Excel
    /// </summary>
    /// <param name="request">Filtros opcionais (datas)</param>
    /// <returns>Arquivo Excel (.xlsx)</returns>
    /// <response code="200">Retorna arquivo Excel</response>
    /// <response code="204">Nenhum registro encontrado no período</response>
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGetReportExcelUseCase getReportExcelUseCase,[FromQuery] RequestReportJson request)
    {
        var excelBytes = await getReportExcelUseCase.Execute(request);

        if (excelBytes == null || excelBytes.Length == 0)
            return NoContent();

        // Nome do arquivo com datas
        var fileName = request.StartDate.HasValue && request.EndDate.HasValue
            ? $"billings_{request.StartDate.Value:yyyy-MM-dd}_{request.EndDate.Value:yyyy-MM-dd}.xlsx"
            : $"billings_{DateTime.Now:yyyy-MM-dd}.xlsx";

        return File(
            fileContents: excelBytes,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: fileName);
    }

    [HttpGet("PDF")]
    public async Task<IActionResult> GetPDF([FromServices] IGetReportPDFUseCase getReportPDFUseCase, [FromQuery] RequestReportJson request)
    {
        var pdfBytes = await getReportPDFUseCase.Execute(request);
        if (pdfBytes == null || pdfBytes.Length == 0)
            return NoContent();

        var fileName = request.StartDate.HasValue && request.EndDate.HasValue
            ? $"billings_{request.StartDate.Value:yyyy-MM-dd}_{request.EndDate.Value:yyyy-MM-dd}.pdf"
            : $"billings_{DateTime.Now:yyyy-MM-dd}.pdf";

        return File(
            fileContents: pdfBytes,
            contentType: "application/pdf",
            fileDownloadName: fileName);
    }
}
