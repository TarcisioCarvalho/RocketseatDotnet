using Microsoft.AspNetCore.Mvc;
using PetFolio.Application.UseCases.Pet.Delete;
using PetFolio.Application.UseCases.Pet.GetAll;
using PetFolio.Application.UseCases.Pet.GetById;
using PetFolio.Application.UseCases.Pet.Register;
using PetFolio.Application.UseCases.Pet.Update;
using PetFolio.Communication.Requests;
using PetFolio.Communication.Response;

namespace PetFolio.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterPetJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status400BadRequest)]
    public IActionResult RegisterPet([FromBody] RequestPetJson request)
    {
        var response = new RegisterPetUseCase().Execute(request);
        return Created(string.Empty, response);
    }
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute]int id,[FromBody] RequestPetJson request) 
    {
        new UpdatePetUseCase().Execute(id, request);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllPetJson),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        var response = new GetAllPetsUseCase().Execute();
        if(response.Pets.Any())
        return Created(string.Empty, response);

        return NoContent();
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponsePetJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute] int id) 
    {
        var response = new GetByIdUseCase().Execute(id);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute]int id)
    {
        new DeleteUseCase().Execute(id);
        return NoContent();
    }
}
