using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Components.Domain.Main.DTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly Handler _handler;

        public UserController(Handler handler)
        {
            _handler = handler;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RequestCreateUser newUser)
        {
            var result = await _handler.CreateUser(newUser);

            return Ok(result);
        }
        [HttpGet("confirmation/{token}")]
        public async Task<IActionResult> Confirm([FromRoute] string token)
        {
            var result = await _handler.ConfirmEmail(token);

            return Ok(result);
        }

       

        [Authorize]
        [HttpPut("change-password-in")]
        public async Task<IActionResult> ChangePasswordLogged(
            [FromHeader] string Authorization,
            [FromBody] RequestPassword newPassword
            )
        {
            string token = Authorization.StartsWith("Bearer ") ? Authorization.Substring(7) : Authorization;
            var result = await _handler.ChangePasswordLogged(token, newPassword);

            return Ok(result);
        }
        [HttpPut("reset-password-out")]
        public async Task<IActionResult> ResetPasswordNotLogged([FromBody] RequestEmail email)
        {
            var result = await _handler.ResetPasswordNotLogged(email);

            return Ok(result);
        }
        [HttpGet("reset-password/{token}")]
        public IActionResult ResetPasswordHtml([FromRoute] string token)
        {
            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>Resetar Senha</title>
                </head>
                <body>
                    <h1>Resetar Senha</h1>
                    <h3>Digite a nova senha</h3>
                    <form method='post' action='/user/reset-password'>
                        <input type='password' name='password' placeholder='Digite a nova senha' />
                        <input type='hidden' name='token' value='{token}' />
                        <button type='submit'>Salvar nova Senha</button>
                    </form>
                </body>
                </html>";

            return Content(htmlContent, "text/html");
        }

        //endpoint from html forms
        [HttpPost("/user/reset-password")]
        public async Task<IActionResult> ResetPasswordHtmlPost(
            [FromForm] string password,
            [FromForm] string token)
        {
            var result = await _handler.ResetPassword(token, password);
            return Ok(result);
        }
    }
}
