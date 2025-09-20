using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
		try
		{
            var response = new RegisterExpenseUseCase().Execute(request);
            return Created(string.Empty, response);
        }
		catch (ArgumentException ex)
		{
            var response = new ResponseErrorJson() { ErrorMessage = ex.Message };
            return BadRequest(response);
        }
     
    }
}
