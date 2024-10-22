using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserHandler _handler;

        public UserController(UserHandler handler)
        {
            _handler = handler;
        }

        [SwaggerOperation(Summary = "Criar um novo usuário.", Description = "Cria um novo usuário no sistema.")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RequestCreateUser newUser)
        {
            var result = await _handler.CreateUser(newUser);

            return Ok(result);
        }
        [SwaggerOperation(Summary = "Confirmar conta.", Description = "Confirma a conta de um usuário com o link enviado por email.")]
        [HttpGet("confirmation/{token}")]
        public async Task<IActionResult> Confirm([FromRoute] string token)
        {
            var result = await _handler.ConfirmEmail(token);

            return Ok(result);
        }

        [SwaggerOperation(Summary = "Alterar senha.", Description = "Altera a senha do usuário autenticado.")]
        [Authorize]
        [HttpPost("change-password-in")]
        public async Task<IActionResult> ChangePasswordLogged(
            [FromHeader] string Authorization,
            [FromBody] RequestPassword newPassword
            )
        {
            string token = Authorization.StartsWith("Bearer ") ? Authorization.Substring(7) : Authorization;
            var result = await _handler.ChangePasswordLogged(token, newPassword);

            return Ok(result);
        }
        [SwaggerOperation(Summary = "Recuperar senha.", Description = "Recupera senha do usuário não autenticado.")]
        [HttpPost("reset-password-out")]
        public async Task<IActionResult> ResetPasswordNotLogged([FromBody] RequestEmail email)
        {
            var result = await _handler.ResetPasswordNotLogged(email);

            return Ok(result);
        }
        [SwaggerOperation(Summary = "Recadastrar senha.", Description = "Endpoint para cadastrar uma nova senha com link vindo do email.")]
        [HttpPost("reset-password/{token}")]
        public async  Task<IActionResult> ResetPassword(
            [FromRoute] string token,
            [FromBody] RequestPassword password)
        {
            var result = await _handler.ResetPassword(token, password.NewPassword);

            return Ok(result);
        }
    }
}
