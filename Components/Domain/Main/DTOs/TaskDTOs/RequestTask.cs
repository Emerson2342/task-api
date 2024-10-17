﻿using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Shared.Entities;

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
        public DateTime? StartTime { get; set; }


        [JsonPropertyName("deadLine")]
        [SwaggerSchema("Data do término da tarefa.")]
        public DateTime? Deadline { get; set; }

        [JsonConstructor]
        public RequestTask()
        {
            
        }
    }
}
