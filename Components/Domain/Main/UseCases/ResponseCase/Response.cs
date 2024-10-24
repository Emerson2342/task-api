
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Pages.UserPages;
using static TaskList.Components.Domain.Main.Entities.TaskEntity;

namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {

        [JsonPropertyName("userInfo")]
        public UserResponse User { get; set; } = new UserResponse();

        [JsonPropertyName("taskList")]
        public TaskEntity TaskList { get; set; }

        [JsonPropertyName("tasksList")]
        public List<TaskEntity> TasksList { get; set; } = [];

        protected Response() { }

        public Response(string massage, int status)
        {
            Message = massage;
            Status = status;
        }
        public Response(string message, User user, string token)
        {
            Message = message;
            Status = 201;
            User.Name = user.Name;
            User.UserId = user.Id;
            User.Token = token;
            User.IsEmailConfirmed = user.IsEmailConfirmed;
            User.Email = user.Email;
        }
        public Response(string message, User user)
        {
            Message = message;
            Status = 201;
            User.Name = user.Name;
            User.UserId = user.Id;
            User.IsEmailConfirmed = user.IsEmailConfirmed;
            User.Email = user.Email;
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
