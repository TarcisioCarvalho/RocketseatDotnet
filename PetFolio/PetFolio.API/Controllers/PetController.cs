using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFolio.Application.UseCases.Pet.Register;
using PetFolio.Communication.Requests;
using PetFolio.Communication.Response;

namespace PetFolio.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterPetJson),StatusCodes.Status201Created)]
    public IActionResult RegisterPet([FromBody] RequestRegisterPetJson request)
    {
        var response = new RegisterPetUseCase().Execute(request);
        return Created(string.Empty,response);
    }
}
