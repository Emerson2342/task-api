﻿using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class TaskEntity : Entity
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("user")]
        public User? User { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } =string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("startTime")]
        public DateOnly StartTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [JsonPropertyName("deadLine")]
        public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        [JsonPropertyName("photoTask")]
        public string PhotoTask { get; set; } = string.Empty;

        [JsonConstructor]
        protected TaskEntity() { }
      
        private TaskEntity(Guid userId, string title, string description, DateOnly startTime, DateOnly deadline, string? photoTask)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Deadline = deadline;
            Title = title;
            Description = description;
            StartTime = startTime;
            Deadline = deadline;
            PhotoTask = photoTask ?? string.Empty;
        }

        public static TaskResult New(Guid userId, string title, string description, DateOnly startTime, DateOnly deadline, string? photoTask)
        {
            if (string.IsNullOrEmpty(title)) return new TaskResult(new Response("Favor preencher o título", 400));

            if (string.IsNullOrEmpty(description)) return new TaskResult(new Response("Favor preencher a descrição da atividade", 400));
            if (startTime < DateOnly.FromDateTime(DateTime.UtcNow)) return new TaskResult(new Response("Data inicial inválida", 400));
            if (deadline < startTime) return new TaskResult(new Response("Data final da atividade não pode ser menor que a data inicial!", 400));

            TaskEntity task = new(userId, title, description, startTime, deadline, photoTask);

            return new TaskResult(new Response("Tarefa adicionada com sucesso!", task), task);
        }

        public static TaskResult Edit(TaskEntity originalTask, RequestTask editTask)
        {
            DateOnly defaultStartTime = DateOnly.FromDateTime(DateTime.Now);
            DateOnly defaultDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

            originalTask.Title = string.IsNullOrEmpty(editTask.Title) ? originalTask.Title : editTask.Title;
            originalTask.Description = string.IsNullOrEmpty(editTask.Description) ? originalTask.Description : editTask.Description;
            originalTask.StartTime = editTask.StartTime == defaultStartTime ? originalTask.StartTime : editTask.StartTime;
            originalTask.Deadline = editTask.Deadline == defaultDeadline ? originalTask.Deadline : editTask.Deadline;
            originalTask.PhotoTask = string.IsNullOrEmpty(editTask.PhotoTask) ? originalTask.PhotoTask : editTask.PhotoTask;

            if (originalTask.StartTime < DateOnly.FromDateTime(DateTime.UtcNow))
                return new TaskResult(new Response("Data inicial inválida", 400));

            if (originalTask.Deadline < originalTask.StartTime)
                return new TaskResult(new Response("Data final da atividade não pode ser menor que a data inicial!", 400));

            return new TaskResult(new Response("Tarefa editada com sucesso!", originalTask), originalTask);
        }

        public class TaskResult
        {
            public Response Response { get; set; }
            public TaskEntity? TaskEntity { get; set; } 
           
            public TaskResult(Response response, TaskEntity taskEntity)
            {
                Response = response;
                TaskEntity = taskEntity;
            }
            public TaskResult(Response response)
            {
                Response = response;
            }

        }

        public class TasksResult
        {
            [JsonPropertyName("response")]
            public Response Response { get; set; }
            [JsonPropertyName("taskEntities")]
            public List<TaskEntity> TaskEntities { get; set; }

          
            public TasksResult(Response response, List<TaskEntity> tasksEntities)
            {
                Response = response;
                TaskEntities = tasksEntities;
            }
        }
    }


}
