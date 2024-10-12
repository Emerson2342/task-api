
namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {
        public ResponseDataUser? ResponseDataUser { get; set; }
        public ResponseDataTask? ResponseDataTask { get; set; }

        public Response() { }

        //not succesfull
        public Response(string massage, int status)
        {
            Message = massage;
            Status = status;
        }

        //succesfull
        public Response(string message, ResponseDataUser responseData)
        {
            Message = message;
            Status = 201;
            ResponseDataUser = responseData;
        }

        public Response(string message, ResponseDataTask responseData)
        {
            Message = message;
            Status = 201;
            ResponseDataTask = responseData;
        }

    }

    public record ResponseDataUser(string Name, string Email, string Token)
    {
    }
    public record ResponseDataTask(string Title, DateTime StartTime, DateTime Deadline)
    {
    }



}
