using BarberBoss.Application.UseCases.Reports.Get;
using BarberBoss.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGetReportExcelUseCase getReportExcelUseCase,[FromQuery] RequestExcelJson request)
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
}
