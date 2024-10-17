using Microsoft.IdentityModel.Tokens;
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
        public User User { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }
        [JsonPropertyName("deadLine")]
        public DateTime? Deadline { get; set; }

        [JsonConstructor]
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

        public static bool CheckEdit(RequestTask task)
        {
            if (string.IsNullOrEmpty(task.Title) 
                || string.IsNullOrEmpty(task.Description)
                || !task.StartTime.HasValue
                || !task.Deadline.HasValue)
                return false;
            return true;
        }

        //public static TaskEntity Edit(TaskEntity taskToEdit)
        //{
        //   originalTask.Title =  !string.IsNullOrEmpty(taskToEdit.Title) ? taskToEdit.Title : originalTask.Title;
        //   originalTask.Description =  !string.IsNullOrEmpty(taskToEdit.Description) ? taskToEdit.Description : originalTask.Description;
        //   originalTask.StartTime =  taskToEdit.StartTime.HasValue ? taskToEdit.StartTime.Value : originalTask.StartTime;
        //   originalTask.Deadline =  taskToEdit.Deadline.HasValue ? taskToEdit.Deadline.Value : originalTask.Deadline;

        //   return originalTask;
        //}

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
