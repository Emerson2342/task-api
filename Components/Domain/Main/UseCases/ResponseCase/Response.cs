
namespace TaskList.Components.Domain.Main.UseCases.ResponseCase
{
    public class Response : Shared.UseCases.Response
    {
        public ResponseData? ResponseData { get; set; }

        protected Response() { }

        //succesfull
        public Response(string massage, int status)
        {
            Message = massage;
            Status = status;
        }

        //succesfull
        public Response(string message, ResponseData responseData)
        {
            Message = message;
            Status = 201;
            ResponseData = responseData;
        }

    }

    public record ResponseData(string name, string email, string token)
    {


    }



}
