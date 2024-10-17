
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {

        [JsonPropertyName("responseDataUser")]
        public User? ResponseDataUser { get; set; }

        [JsonPropertyName("taskList")]
        public TaskEntity? TaskList { get; set; }

        [JsonPropertyName("tasksList")]
        public List<TaskEntity>? TasksList { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        //[JsonConstructor]
        public Response() { }


       // [JsonConstructor]
        //not succesfull
        public Response(string massage, int status)
        {
            Message = massage;
            Status = status;
        }

        //[JsonConstructor]
        //succesfull
        public Response(string message, User responseData)
        {
            Message = message;
            Status = 201;
            ResponseDataUser = responseData;
        }
        
        //[JsonConstructor]
        public Response(string message, string token)
        {
            Message = message;
            Status = 201;
            Token = token;
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
