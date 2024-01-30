using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Dto
{
    public class GenericResponse<T> : BasicResponse
    {
        public T Data { get; set; }
        public IDictionary<string, object> Meta { get; set; }
    }
}
