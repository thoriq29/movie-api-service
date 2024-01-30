using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Responses
{
    public class GenericListResponse<T> : IGenericListResponse<T>
    {
        public ICollection<T> Data { get; set; }
        public IDictionary<string, object> Meta { get; set; }
        public IError Error { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
