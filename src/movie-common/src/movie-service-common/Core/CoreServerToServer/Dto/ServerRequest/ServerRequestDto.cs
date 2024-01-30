using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Dto.ServerRequest
{
    public class ServerRequestDto<T>
    {
        public string Url { get; set; }
        public T Data { get; set; }
    }
}
