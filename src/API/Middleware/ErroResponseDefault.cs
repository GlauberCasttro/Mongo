using System.Net;

namespace API.Middleware
{
    public class ErroResponseDefault
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}