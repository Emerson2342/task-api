using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly UserHandler _handler;

        public HomeController(UserHandler handler)
        {
            _handler = handler;
        }

        [SwaggerOperation(Summary = "Entrar na conta.", Description = "Entra na conta com o login e senha cadastrados.")]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin login)
        {
            var result = await _handler.Login(login);

            return Ok(result);
        }


    }
}
