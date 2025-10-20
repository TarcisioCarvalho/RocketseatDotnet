using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExcel()
    {
        byte[] fileContents = new byte[1];
        if (fileContents.Length == 0)
            return NotFound();

        return File(fileContents,MediaTypeNames.Application.Octet,"report.xlsx");
    }
}
