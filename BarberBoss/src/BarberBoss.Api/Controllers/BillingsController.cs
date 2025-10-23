using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BillingsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] IDbConnection dbConnection)
    {
        var result = await dbConnection.QueryFirstAsync<DateTime>("Select Now()");
        
        return Ok("Billing created");
    }
}
