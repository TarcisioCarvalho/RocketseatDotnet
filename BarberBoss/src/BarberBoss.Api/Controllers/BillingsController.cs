using BarberBoss.Application.UseCases.Billings.Delete;
using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Application.UseCases.Billings.Update;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BillingsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterBillingJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IRegisterBillingUseCase registerBillingUseCase, [FromBody] RequestBillingJson request)
    {
        var response = await registerBillingUseCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBillingsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBillings([FromServices] IGetAllBillingUseCase getAllBillingUseCase, [FromQuery] RequestBillingsJson request)
    {
        var response = await getAllBillingUseCase.Execute(request);
        if (response.Billings.Any() is false)
            return NoContent();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBillingById([FromServices] IGetBillingByIdUseCase getBillingByIdUseCase, [FromRoute] Guid id)
    {
        var response = await getBillingByIdUseCase.Execute(id);
        if (response is null)
            return NotFound();
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateBillingUseCase updateBillingUseCase, [FromRoute] Guid id, [FromBody] RequestBillingJson request)
    {
        await updateBillingUseCase.Execute(id, request);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteBillingUseCase deleteBillingUseCase, [FromRoute] Guid id)
    {
        await deleteBillingUseCase.Execute(id);
        return NoContent();
    }
}
