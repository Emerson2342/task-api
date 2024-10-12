using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Components.Domain.Main.DTOs.TaskDTOs
{
    public class RequestCreateTask
    {

        [SwaggerSchema("Id do usuário.")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Favor preencher o campo TÍTULO")]
        [SwaggerSchema("Nome da tarefa.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Favor preencher o campo DESCRIÇÃO")]
        [SwaggerSchema("Descrição da tarefa.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Favor preencher o campo INÍCIO DA TAREFA")]
        [SwaggerSchema("Data do início da tarefa.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Favor preencher o campo TÉRMINO DA TAREFA")]
        [SwaggerSchema("Data do término da tarefa.")]
        public DateTime Deadline { get; set; }

    }
}
