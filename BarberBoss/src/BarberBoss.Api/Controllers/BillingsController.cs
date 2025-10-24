using BarberBoss.Communication.Requests;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BillingsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestRegisterBillingJson request)
    {
        return Ok(request);
    }
}
