using System.Net;
using Service.Core.Interfaces.Log;

namespace Service.Core.Interfaces.Framework
{
    public interface IBasicResponse
    {
        public IError Error { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
