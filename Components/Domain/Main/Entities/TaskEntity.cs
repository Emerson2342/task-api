﻿using Microsoft.IdentityModel.Tokens;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class TaskEntity : Entity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }

        public TaskEntity() { }

        private TaskEntity(Guid userId, string title, string description, DateTime startTime, DateTime deadline)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Deadline = deadline;
            Title = title;
            Description = description;
            StartTime = startTime;
            Deadline = deadline;
        }

        public static TaskResult With(Guid userId, string title, string description, DateTime startTime, DateTime deadline)
        {
            if (string.IsNullOrEmpty(title)) return new TaskResult(new Response("Favor preencher o título", 400), null);

            if (string.IsNullOrEmpty(description)) return new TaskResult(new Response("Favor preencher a descrição da atividade", 400), null);
            if (startTime > DateTime.UtcNow) return new TaskResult(new Response("Data inicial inválida", 400), null);
            if (deadline < DateTime.UtcNow) return new TaskResult(new Response("Data final da atividade inválida!", 400), null);

            TaskEntity task = new(userId, title, description, startTime, deadline);

            return new TaskResult(new Response("Atividade adicionada com sucesso!", task), task);
        }

        public static TaskEntity Edit(TaskEntity originalTask, RequestTask taskToEdit)
        {
           originalTask.Title =  !string.IsNullOrEmpty(taskToEdit.Title) ? taskToEdit.Title : originalTask.Title;
           originalTask.Description =  !string.IsNullOrEmpty(taskToEdit.Description) ? taskToEdit.Description : originalTask.Description;
           originalTask.StartTime =  taskToEdit.StartTime.HasValue ? taskToEdit.StartTime.Value : originalTask.StartTime;
           originalTask.Deadline =  taskToEdit.Deadline.HasValue ? taskToEdit.Deadline.Value : originalTask.Deadline;
            
           return originalTask;
        }

        public class TaskResult
        {
            public Response Response { get; set; }
            public TaskEntity TaskEntity { get; set; }

            public TaskResult(Response response, TaskEntity taskEntity)
            {
                Response = response;
                TaskEntity = taskEntity;
            }
        }
    }


}
