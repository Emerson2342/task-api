using System.ComponentModel.DataAnnotations;

namespace TaskList.Components.Domain.Main.DTOs
{

    public record RequestCreateUser()
    {
        [Required(ErrorMessage = "Favor, preencher o campo NOME")]
        public string Name { get; set; } =string.Empty;
        [Required(ErrorMessage = "Favor, preencher o campo EMAIL")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Favor, preencher o campo SENHA")]
        public string Password { get; set; } = string.Empty;
    }
}
