using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace TaskList.Components.Domain.Shared.Entities
{
    public abstract class Entity
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Id da tarefa.")]
        public Guid Id { get; set; }
        protected Entity() {         
            Id = Guid.NewGuid();        
        }
    }
}
