
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {
        public User? ResponseDataUser { get; set; }
        public TaskEntity? TaskList { get; set; }
        public List<TaskEntity>? TasksList { get; set; }
        public string? Token { get; set; }

        public Response() { }

        //not succesfull
        public Response(string massage, int status)
        {
            Message = massage;
            Status = status;
        }

        //succesfull
        public Response(string message, User responseData)
        {
            Message = message;
            Status = 201;
            ResponseDataUser = responseData;
        }public Response(string message, string token)
        {
            Message = message;
            Status = 201;
            Token = token;
        }

        public Response(string message, TaskEntity responseData)
        {
            Message = message;
            Status = 201;
            TaskList = responseData;
        }
        public Response(string message, List<TaskEntity> tasksList)
        {
            Message = message;
            Status = 201;
            TasksList = tasksList;
        }

    }

}
