using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Components.Domain.Main.DTOs.UserDTOs
{

    public class RequestCreateUser
    {
        
        [SwaggerSchema("Nome do usuário.")]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("Email do usuário.")]
        public string Email { get; set; } = string.Empty;
        [SwaggerSchema("Senha do usuário.")]
        public string Password { get; set; } = string.Empty;
    }
}
