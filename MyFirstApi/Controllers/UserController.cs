using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Comunnication.Requests;
using MyFirstApi.Comunnication.Response;

namespace MyFirstApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = new List<string> { "Alice", "Bob", "Charlie" };
        return Ok(users);
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    public IActionResult GetUserById([FromRoute] int id)
    {
        var user = $"User{id}";
        if(id < 1 || id > 3)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
    public IActionResult RegisterUser([FromBody] RequestRegisterUserJson request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Invalid user data");
        }
        var response = new ResponseRegisterUserJson
        {
            Id = 4,
            Name = request.Name
        };
        // Here you would normally save the user to a database
        return Created(string.Empty, response);
    }
}
