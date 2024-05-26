using System.Net;

namespace VirtualClients_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>? Messages { get; set; }
        public Object? Result { get; set; }
    }
}
