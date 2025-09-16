using GerenciadorLivraria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GerenciadorLivrariaController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddBook([FromBody] Book book)
    {
        var response = new
        {
            Message = "Book added successfully",
            Book = book
        };
        return Created(string.Empty, response);
    }
}
