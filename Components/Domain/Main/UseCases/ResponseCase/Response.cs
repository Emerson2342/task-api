
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {
        public User? ResponseDataUser { get; set; }
        public TaskEntity? ResponseDataTask { get; set; }

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
        }

        public Response(string message, TaskEntity responseData)
        {
            Message = message;
            Status = 201;
            ResponseDataTask = responseData;
        }

    }

}
