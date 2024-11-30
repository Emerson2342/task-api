using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace TaskList.Components.Domain.Main.DTOs.TaskDTOs
{
    public class RequestTask
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Id da tarefa.")]
        public Guid Id { get; set; }

        [JsonPropertyName("userId")]
        [SwaggerSchema("Id do usuário.")]
        public Guid UserId { get; set; }

        [JsonPropertyName("title")]
        [SwaggerSchema("Nome da tarefa.")]
        public string Title { get; set; } = string.Empty;


        [JsonPropertyName("description")]
        [SwaggerSchema("Descrição da tarefa.")]
        public string Description { get; set; } = string.Empty;


        [JsonPropertyName("startTime")]
        [SwaggerSchema("Data do início da tarefa.")]
        public DateOnly StartTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        [JsonPropertyName("deadLine")]
        [SwaggerSchema("Data do término da tarefa.")]
        public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        [JsonPropertyName("photo_task")]
        [SwaggerSchema("base64 da foto da tarefa")]
        public string PhotoTask { get; set; } = string.Empty;

        [JsonConstructor]
        public RequestTask()
        {
            
        }
    }
}
