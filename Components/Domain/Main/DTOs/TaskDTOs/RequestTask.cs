﻿using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.DTOs.TaskDTOs
{
    public class RequestTask : Entity
    {
        
        [SwaggerSchema("Id do usuário.")]
        public Guid UserId { get; set; }
        [SwaggerSchema("Nome da tarefa.")]
        public string Title { get; set; } = string.Empty;

        [SwaggerSchema("Descrição da tarefa.")]
        public string Description { get; set; } = string.Empty;

        [SwaggerSchema("Data do início da tarefa.")]
        public DateTime? StartTime { get; set; }

        [SwaggerSchema("Data do término da tarefa.")]
        public DateTime? Deadline { get; set; }


    }
}