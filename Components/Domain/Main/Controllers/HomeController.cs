using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskList.Components.Domain.Main.DTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly Handler _handler;

        public HomeController(Handler handler)
        {
            _handler = handler;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateUser([FromBody] RequestCreateUser newUser)
        {
            var result = await _handler.CreateUser(newUser);

            return Ok(result);
        }
        [HttpGet("/v1")]
        public IActionResult Get()
        {
          

            return Ok("result");
        }
    }
}
