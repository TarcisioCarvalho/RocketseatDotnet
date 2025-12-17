using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.ADMIN)]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly month, [FromServices] IGenerateExpensesReportExcelUseCase generateExpensesReportExcelUseCase)
    {
        byte[] fileContents = await generateExpensesReportExcelUseCase.Execute(month);
        if (fileContents.Length == 0)
            return NotFound();

        return File(fileContents, MediaTypeNames.Application.Octet, "report.xlsx");
    }
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromQuery] DateOnly month, [FromServices] IGenerateExpensesReportPdfUseCase generateExpensesReportPdfUseCase)
    {
        byte[] fileContents = await generateExpensesReportPdfUseCase.Execute(month);
        if (fileContents.Length == 0)
            return NotFound();
        return File(fileContents, MediaTypeNames.Application.Pdf, "report.pdf");
    }
}
