using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Components.Domain.Main.DTOs
{

    public class RequestCreateUser()
    {
        [Required(ErrorMessage = "Favor, preencher o campo NOME")]
        [SwaggerSchema("Nome do usuário.")]
        public string Name { get; set; } =string.Empty;
        [Required(ErrorMessage = "Favor, preencher o campo EMAIL")]
        [SwaggerSchema("Email do usuário.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Favor, preencher o campo SENHA")]
        [SwaggerSchema("Senha do usuário.")]
        public string Password { get; set; } = string.Empty;
    }
}
