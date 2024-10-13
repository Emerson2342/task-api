using Swashbuckle.AspNetCore.Annotations;

namespace TaskList.Components.Domain.Main.DTOs.TaskDTOs
{
    public class RequestEditTask
    {
        [SwaggerSchema("Nome da tarefa.")]
        public string Title { get; set; } = string.Empty;
    }
}
