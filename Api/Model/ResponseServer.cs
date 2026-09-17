using System.Net;

namespace Api.Model
{
    public class ResponseServer
    {

        public ResponseServer()
        {
            this.IsSuccess = true;
            this.ErrorMessages = new();
        }
        public bool IsSuccess {get; set;}
        public HttpStatusCode HttpStatus  {get; set;}
        public List<string> ErrorMessages {get; set;}
        public object Result {get; set;}
    }
}