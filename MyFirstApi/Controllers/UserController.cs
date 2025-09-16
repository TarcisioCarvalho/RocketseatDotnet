using Microsoft.AspNetCore.Mvc;

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
}
