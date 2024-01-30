using Service.Core.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Framework
{
    public interface IServiceApiResult : IServiceApiResult<string> { }

    public interface IServiceApiResult<T>
    {
        public ResultStatus ResultStatus { get; set; }
        public IGenericResponse<T> BasicResponse { get; set; }
    }
}
