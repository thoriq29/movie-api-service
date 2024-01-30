using System.Net;

namespace Service.ServerToServer.Dto.ServerRequest
{
    public class ServerResponseDto<T>
    {
        public string Url { get; set; }
        public T DataRequest { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
