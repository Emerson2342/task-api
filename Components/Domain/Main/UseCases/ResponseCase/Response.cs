
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Pages.UserPages;
using static TaskList.Components.Domain.Main.Entities.TaskEntity;

namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {

        [JsonPropertyName("user")]
        public User? User { get; set; }

        [JsonPropertyName("taskList")]
        public TaskEntity? TaskList { get; set; }

        [JsonPropertyName("tasksList")]
        public List<TaskEntity>? TasksList { get; set; }

        protected Response() { }

        public Response(string message)
        {
            Message = message;
        }

        public Response(string message, int status)
        {
            Message = message;
            Status = status;
        }
        public Response(string message, User user, string token)
        {
            Message = message;
            Status = 201;
            User = new User
            {
                Name = user.Name,
                Id = user.Id,
                Token = token,
                IsEmailConfirmed = user.IsEmailConfirmed,
                Email = user.Email
            };
        }
        public Response(string message, User user)
        {
            Message = message;
            Status = 201;

            User = new()
            {
                Name = user.Name,
                Id = user.Id,
                IsEmailConfirmed = user.IsEmailConfirmed,
                Email = user.Email
            };
        }

        //[JsonConstructor]
        public Response(string message, TaskEntity responseData)
        {
            Message = message;
            Status = 201;
            TaskList = responseData;
        }
        [JsonConstructor]
        public Response(string message, List<TaskEntity> tasksList)
        {
            Message = message;
            Status = 201;
            TasksList = tasksList;
        }

    }

}
